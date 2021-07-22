using Domain.Entities;
using Infrastructure.Services.ConfigServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly EmailServiceOptions config;
        public EmailSender(UserManager<ApplicationUser> userManager, IOptions<EmailServiceOptions> options)
        {
            this.userManager = userManager;
            config = options.Value; ;
        }

        public async Task<Response> SendEmailAsync(EmailSetting emailSetting)
        {
            try
            {
                var client = new SendGridClient(config.APIKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(config.HostAddress, emailSetting.SenderName ?? config.Sender),
                    Subject = emailSetting.Subject ?? config.Caption,
                };
                msg.AddContent(MimeType.Text, emailSetting.Message);
                msg.AddTo(new EmailAddress(emailSetting.To, emailSetting.RecipientName));
                var response = await client.SendEmailAsync(msg, emailSetting.CancellationToken).ConfigureAwait(false);

                return response;
            }
            catch (Exception) { throw; }
        }
    }
}
