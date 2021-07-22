using HeartInsuranceWeb.DTOs.Requests;
using HeartInsuranceWeb.DTOs.Responses;
using System.Threading.Tasks;

namespace HeartInsuranceWeb.Services.HeartInsuranceMicroservice.Interfaces
{
    public interface IHeartInsuranceService
    {
        Task<ServiceResponse> SignUp(CreateAccountDTORequest request);
        Task<ServiceResponse> SignIn(LoginDTORequest request);
        Task<ServiceResponse> ConfirmEmail(EmailConfirmationDTORequest request);
        Task<ServiceResponse> ValidatePassCode(ValidatePasscodeDTORequest request);
    }
}
