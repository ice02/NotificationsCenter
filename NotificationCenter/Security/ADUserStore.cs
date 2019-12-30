using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationCenter.Security
{
    /// <summary>
    /// We define the FindByIdAsync method to get a user populated thanks to LDAP
    /// </summary>
    /// <typeparam name="T">T is MyUser type</typeparam>
    public class ADUserStore<T> : IUserStore<ADUser>, IUserPasswordStore<ADUser>
    {
        // Search and create a user from Active Directory
        public async Task<ADUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (userId == null)
                return await Task.Run(() => new ADUser()
                {
                    UserName = "Anonymous",
                    Id = "Anonymous"
                });

            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var directoryUser = UserPrincipal.FindByIdentity(context, userId);
                if (directoryUser != null)
                {
                    return await Task.Run(() => new ADUser()
                    {
                        Id = userId,
                        UserName = userId,
                        Email = directoryUser.EmailAddress,
                        FirstName = directoryUser.GivenName,
                        LastName = directoryUser.Surname,
                        DisplayName = directoryUser.Name
                    }
                    );
                }
                else
                    return await Task.Run(() => new ADUser()
                    {
                        UserName = "Anonymous",
                        Id = "Anonymous"
                    });
            }
        }

        public void Dispose()
        {

        }

        #region Not Implemented

        public Task<string> GetUserIdAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ADUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ADUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ADUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ADUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ADUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
