using Application.Interfaces.Application;
using Domain.Entities;
using Infrastructure.Services.ConfigServices;
using Infrastructure.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class EmailManager : IEmailManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;
        private readonly EmailServiceOptions config;

        public EmailManager(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IOptions<EmailServiceOptions> configOptions)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            config = configOptions.Value;
        }
        public async Task<object> EmailToken(ApplicationUser user)
        {
            var currentUser = await userManager.FindByEmailAsync(user.Email);
            if (currentUser != null)
                return await userManager.GenerateEmailConfirmationTokenAsync(user);

            return null;
        }

        public async void SendEmailConfirmationMail(ApplicationUser user)
        {
            await emailSender.SendEmailAsync(new EmailSetting
            {
                To = user.Email,
                RecipientName = user.EncFullName,
                SenderName = config.Sender,
                Message = config.ConfirmationMessage + string.Concat(Enumerable.Repeat(Environment.NewLine, 2)) + await EmailToken(user),
                Subject = "Email Confirmation",
                CancellationToken = CancellationToken.None
            });
        }
    }
}