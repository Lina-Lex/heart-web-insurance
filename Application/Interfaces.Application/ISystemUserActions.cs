using Application.Common.Responses;
using Application.Common.Models.UserModels;
using System.Threading.Tasks;

namespace Application.Interfaces.Application
{
    public interface ISystemUserActions
    {
        Task<ResponseModel> SignUp(ApplicationUserModel model);
        Task<ResponseModel> SignIn(string email);
        Task<ResponseModel> VerifyPassCodeThenLogin(string email, string generatedValue);
        Task<ResponseModel> EmailConfirmation(string email, string token);
    }
}
