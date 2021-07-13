using Application.Common.Responses;
using Application.Common.Models.UserModels;
using System.Threading.Tasks;

namespace Application.Interfaces.Application
{
    public interface ISystemUserActions
    {
        Task<ResponseModel> SignUp(ApplicationUserModel model);
        //Task<ResponseModel> SignIn(LoginCommand command);
    }
}
