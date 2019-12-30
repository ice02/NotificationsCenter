using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Security
{
    public class ADSignInManager : SignInManager<ADUser>
    {
        private IHttpContextAccessor _ContextAccessor { get; set; }
        public ADSignInManager(ADUserManager userManager,
                                IHttpContextAccessor contextAccessor,
                                IUserClaimsPrincipalFactory<ADUser> claimsFactory,
                                IAuthenticationSchemeProvider schemeProvider,
                                IOptions<IdentityOptions> optionsAccessor = null
                                )
                : base(userManager, contextAccessor, claimsFactory, optionsAccessor, null, schemeProvider)
        {
            _ContextAccessor = contextAccessor;
        }

        public async override Task SignOutAsync()
        {
            await _ContextAccessor.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
