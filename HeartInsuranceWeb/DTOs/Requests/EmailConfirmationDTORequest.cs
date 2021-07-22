using Newtonsoft.Json;

namespace HeartInsuranceWeb.DTOs.Requests
{
    public class EmailConfirmationDTORequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
