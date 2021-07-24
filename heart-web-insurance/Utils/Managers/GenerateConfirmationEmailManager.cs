using Domain.Entities;
using Infrastructure.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.Managers
{
    public class GenerateConfirmationEmailManager : IConfirmationEmailManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GenerateConfirmationEmailManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<object> GenerateEmailUrl(ApplicationUser user)
        {
            var currentUser = await userManager.FindByEmailAsync(user.Email);
            if(currentUser != null)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                //var callbackUrl = UrlHepler(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                return token;
            }
            return null;
        }
    }
}
