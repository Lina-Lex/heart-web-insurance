using Newtonsoft.Json;

namespace HeartInsurance.Web.DTOs.Requests
{
    public class EmailConfirmationDTORequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("confirmationToken")]
        public string Token { get; set; }
    }
}
