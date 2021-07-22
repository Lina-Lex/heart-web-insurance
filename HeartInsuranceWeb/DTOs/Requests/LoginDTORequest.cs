using Newtonsoft.Json;

namespace HeartInsuranceWeb.DTOs.Requests
{
    public class LoginDTORequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
