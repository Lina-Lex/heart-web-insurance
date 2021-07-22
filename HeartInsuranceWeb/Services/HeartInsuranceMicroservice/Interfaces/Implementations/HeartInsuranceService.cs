using HeartInsuranceWeb.DTOs.Requests;
using HeartInsuranceWeb.DTOs.Responses;
using HeartInsuranceWeb.Services.CleintConfig;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using HttpClientHelperSolutions;
using System.Collections.Generic;

namespace HeartInsuranceWeb.Services.HeartInsuranceMicroservice.Interfaces.Implementations
{
    public class HeartInsuranceService : IHeartInsuranceService
    {
        private readonly HeartInsurnaceService serviceConfig;
        private readonly ClientHelper clientHelper;
        private readonly ILogger<HeartInsuranceService> logger;
        private readonly Uri HeartInsuranceServiceBaseUrl;

        public HeartInsuranceService(ILogger<HeartInsuranceService> logger, IOptions<HeartInsurnaceService> serviceOptions, 
            ClientHelper client)
        {
            serviceConfig = serviceOptions.Value;
            clientHelper = client;
            this.logger = logger;
            HeartInsuranceServiceBaseUrl = new Uri(serviceConfig.BaseAddressUrl);
        }

        public async Task<ServiceResponse> SignIn(LoginDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                var path = string.Format(string.Concat(HeartInsuranceServiceBaseUrl, serviceConfig.SignInEndpoint));
                logger.LogInformation($"{nameof(SignIn)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                return response = await clientHelper.PostAsync<ServiceResponse, LoginDTORequest>(request, path, null, null);
            }
            catch (Exception ex)
            {
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }
        }

        public async Task<ServiceResponse> SignUp(CreateAccountDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                var path = string.Format(string.Concat(HeartInsuranceServiceBaseUrl, serviceConfig.SignUpEndpoint));
                logger.LogInformation($"{nameof(SignUp)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                return response = await clientHelper.PostAsync<ServiceResponse, CreateAccountDTORequest>(request, path, null, null);
            }
            catch (Exception ex)
            {
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }
        }

        public async Task<ServiceResponse> ConfirmEmail(EmailConfirmationDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                var tokenHeader = new Dictionary<string, string>
                {
                    ["token"] = request.Token
                };

                var path = string.Format(string.Concat(HeartInsuranceServiceBaseUrl, serviceConfig.EmailConfirmationEndpoint));
                logger.LogInformation($"{nameof(ConfirmEmail)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                return response = await clientHelper.PostAsync<ServiceResponse, EmailConfirmationDTORequest>(request, path, null, tokenHeader);
            }
            catch (Exception ex)
            {
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }
        }

        public async Task<ServiceResponse> ValidatePassCode(ValidatePasscodeDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                var path = string.Format(string.Concat(HeartInsuranceServiceBaseUrl, serviceConfig.PassCodeVerificationEndpoint));
                logger.LogInformation($"{nameof(ValidatePassCode)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                return response = await clientHelper.PostAsync<ServiceResponse, ValidatePasscodeDTORequest>(request, path, null, null);
            }
            catch (Exception ex)
            {
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }
        }
    }
}
