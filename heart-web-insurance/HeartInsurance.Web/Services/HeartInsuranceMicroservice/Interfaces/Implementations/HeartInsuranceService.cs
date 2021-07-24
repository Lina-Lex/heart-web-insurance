using HeartInsurance.Web.DTOs.Requests;
using HeartInsurance.Web.DTOs.Responses;
using HeartInsurance.Web.Services.CleintConfig;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Mime;
using System.Threading;

namespace HeartInsurance.Web.Services.HeartInsuranceMicroservice.Interfaces.Implementations
{
    public class HeartInsuranceService : IHeartInsuranceService
    {
        private readonly HeartInsurnaceService serviceConfig;
        private readonly HttpClient _client;
        private readonly ILogger<HeartInsuranceService> logger;
        private const string MEDIA_TYPE = MediaTypeNames.Application.Json;

        public HeartInsuranceService(ILogger<HeartInsuranceService> logger, IOptions<HeartInsurnaceService> serviceOptions, 
            IHttpClientFactory clientFactory)
        {
            serviceConfig = serviceOptions.Value;
            this.logger = logger;
            _client = clientFactory.CreateClient(serviceConfig.ClientName);
        }

        public async Task<LoginDTOResponse> SignIn(LoginDTORequest request)
        {
            var response = new LoginDTOResponse();
            try
            {
                logger.LogInformation($"{nameof(SignIn)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.SignInEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<LoginDTOResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                        response.Data = jsonRespose.Data;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(CreateAccountDTORequest request)
        {
            var response = new SignUpResponse();
            try
            {
                logger.LogInformation($"{nameof(SignUp)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.SignUpEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<SignUpResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Data = jsonRespose.Data;
                        response.Message = jsonRespose.Message;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<ServiceResponse> ConfirmEmail(EmailConfirmationDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                logger.LogInformation($"{nameof(ConfirmEmail)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.EmailConfirmationEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<SignUpResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }

        public async Task<ServiceResponse> ValidatePassCode(ValidatePasscodeDTORequest request)
        {
            var response = new ServiceResponse();
            try
            {
                logger.LogInformation($"{nameof(ValidatePassCode)} REQUEST INITIATED. THE REQUEST PAYLOAD => {request}");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                var result = await _client.PostAsync(serviceConfig.PassCodeValidationEndpoint, content, CancellationToken.None);
                if (result.IsSuccessStatusCode)
                {
                    var responseResult = await result.Content.ReadAsStringAsync();
                    var jsonRespose = JsonConvert.DeserializeObject<ServiceResponse>(responseResult);

                    if (jsonRespose.Status.Equals(true))
                    {
                        response.Status = jsonRespose.Status;
                        response.Message = jsonRespose.Message;
                    }
                    response.Status = jsonRespose.Status;
                    response.Message = jsonRespose.Message;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Client request failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message.ToString();
                logger.LogError($"The Response Payload ==> {response} with Errors ===> {ex.Message}");
                throw;
            }

            return response;
        }
    }
}
