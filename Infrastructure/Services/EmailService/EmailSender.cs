using Domain.Entities;
using Infrastructure.Services.ConfigServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        //protected readonly IConfiguration config;
        private readonly Configuration config;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly LinkGenerator linkGenerator;

        public EmailSender(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor,
            LinkGenerator linkGenerator)
        {
            //this.config = config;
            config = Configuration.GetInstance;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
            this.linkGenerator = linkGenerator;
        }

        public async Task<Response> SendEmailAsync(EmailSetting emailSetting)
        {
            try
            {
                var client = new SendGridClient(config.SendGridEmailKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(config.SendGridEmailHostAddress, emailSetting.SenderName),
                    Subject = emailSetting.Subject ?? config.SendGridEmailCaption,
                };
                msg.AddContent(MimeType.Text, emailSetting.Message);
                msg.AddTo(new EmailAddress(emailSetting.To, emailSetting.RecipientName));
                var response = await client.SendEmailAsync(msg, emailSetting.CancellationToken).ConfigureAwait(false);

                return response;
            }
            catch (Exception) { throw; }
        }
        public async void SendConfirmationMail(ApplicationUser user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = linkGenerator.GetPathByName(contextAccessor.HttpContext,
                endpointName: "ConfirmEmail",
                pathBase: null,
                values: new { token, email = user.Email });

            WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = confirmationLink;

            await SendEmailAsync(new EmailSetting
            {
                To = user.Email,
                RecipientName = user.FullName,
                SenderName = config.SendGridSenderName,
                Message = config.EmailConfirmationMessage + $" by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>",
                Subject = "Email Confirmation",
                CancellationToken = CancellationToken.None
            });
        }
    }
}
