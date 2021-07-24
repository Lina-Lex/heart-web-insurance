using Domain.Entities;
using SendGrid;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.EmailService
{
    public interface IEmailSender
    {
        Task<Response> SendEmailAsync(EmailSetting emailSetting);
    }
    public class EmailSetting
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string SenderName { get; set; } = default;
        public string RecipientName { get; set; } = default;
        public CancellationToken CancellationToken { get; set; }
    }
}
