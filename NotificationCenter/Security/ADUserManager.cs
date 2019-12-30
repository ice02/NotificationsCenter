using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace NotificationCenter.Security
{
    /// <summary>
    /// The custom user manager will check user password in LDAP 
    /// and will retrieve the user roles by mapping LDAP groups to applicative roles
    /// 
    /// The role store is not used and we're doing a custom mapping here
    /// </summary>
    public class ADUserManager : UserManager<ADUser>
    {
        private const string LDAP_VIEWER_GROUP_NAME = "[Your LDAP Group Name]";
        private const string LDAP_ADMIN_GROUP_NAME = "[Your LDAP Group Name]";

        public ADUserManager(
            IUserStore<ADUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ADUser> passwordHasher,
            IEnumerable<IUserValidator<ADUser>> userValidators,
            IEnumerable<IPasswordValidator<ADUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors
            //IEnumerable<IUserTokenProvider<ADUser>> tokenProviders,
            //IHttpContextAccessor contextAccessor
            ) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                null, null)
        {

        }

        public async override Task<bool> CheckPasswordAsync(ADUser user, string password)
        {
            return await Task.Run(() => IsAuthenticated(user.UserName, password));
        }

        public async override Task<IList<string>> GetRolesAsync(ADUser user)
        {
            return await Task.Run(() => GetUserMyRoles(user.UserName));
        }

        // Check LDAP if user is in a GROUP
        private List<string> GetUserMyRoles(string username)
        {
            var myRoles = new List<string>();
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    if (IsUserInGroup(context, username, LDAP_VIEWER_GROUP_NAME))
                        myRoles.Add(GetRoleFromGroup(LDAP_VIEWER_GROUP_NAME));

                    if (IsUserInGroup(context, username, LDAP_ADMIN_GROUP_NAME))
                        myRoles.Add(GetRoleFromGroup(LDAP_ADMIN_GROUP_NAME));

                    return myRoles;
                }
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        // Check LDAP if user is in a GROUP
        private bool IsUserInGroup(PrincipalContext context, string user, string group)
        {
            bool found = false;
            try
            {
                GroupPrincipal p = GroupPrincipal.FindByIdentity(context, group);
                UserPrincipal u = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, user);

                found = p.GetMembers(true).Contains(u);
            }
            catch (Exception)
            {
                found = false;
            }

            return found;
        }

        // Map LDAP Group to application Role
        private string GetRoleFromGroup(string group)
        {
            if (group == LDAP_VIEWER_GROUP_NAME)
                return "VIEWER";

            if (group == LDAP_ADMIN_GROUP_NAME)
                return "ADMINISTRATOR";

            else throw new Exception("Undefined LDAP Group");
        }

        // Validate username / password in LDAP
        private bool IsAuthenticated(string username, string pwd)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    return context.ValidateCredentials(username, pwd);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}