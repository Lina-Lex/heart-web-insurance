using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Application
{
    public interface IEmailManager<T> where T: ApplicationUser
    {
        Task<object> EmailToken(T user);
        void SendEmailConfirmationMail(T user);
    }
}
