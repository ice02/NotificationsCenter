using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NotificationCenter.Web.Security
{
    public class AutoLoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AutoLoginMiddleware(RequestDelegate next, ILogger<AutoLoginMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<IntranetUser> userManager, //IUserService userService, 
            SignInManager<IntranetUser> signInManager)
        {
            if (signInManager.IsSignedIn(context.User))
            {
                _logger.LogInformation("User already signed in");
            }
            else
            {
                if (context.User.Identity as WindowsIdentity != null)
                {
                    _logger.LogInformation($"User with Windows Login {context.User.Identity.Name} needs to sign in");
                    var windowsLogin = context.User.Identity.Name;


                    var user = userManager.Users.FirstOrDefault(u => u.NormalizedWindowsLogin == windowsLogin.ToUpperInvariant());

                    if (user != null)
                    {
                        await signInManager.SignInAsync(user, true, "automatic");
                        _logger.LogInformation($"User with id {user.Id}, name {user.UserName} successfully signed in");

                        // Workaround
                        context.Items["IntranetUser"] = user;
                    }
                    else
                    {
                        _logger.LogInformation($"User cannot be found in identity store.");
                        throw new System.InvalidOperationException($"user not found.");
                    }
                }
            }

            // Pass the request to the next middleware
            await _next(context);
        }
    }
}
