using HeartInsurance.Web.DTOs.Requests;
using HeartInsurance.Web.DTOs.Responses;
using System.Threading.Tasks;

namespace HeartInsurance.Web.Services.HeartInsuranceMicroservice.Interfaces
{
    public interface IHeartInsuranceService
    {
        Task<SignUpResponse> SignUp(CreateAccountDTORequest request);
        Task<LoginDTOResponse> SignIn(LoginDTORequest request);
        Task<ServiceResponse> ConfirmEmail(EmailConfirmationDTORequest request);
        Task<ServiceResponse> ValidatePassCode(ValidatePasscodeDTORequest request);
        Task<PatientResponse> Patient();
    }
}
