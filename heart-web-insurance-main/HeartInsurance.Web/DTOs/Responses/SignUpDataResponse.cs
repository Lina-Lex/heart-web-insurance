using System.Text.Json.Serialization;

namespace HeartInsurance.Web.DTOs.Responses
{
    public class SignUpDataResponse : BaseDataResponse
    {
        [JsonPropertyName("confirmationToken")]
        public string Token { get; set; }
    }

    public class BaseDataResponse
    {
        [JsonPropertyName("applicationUserId")]
        public string ApplicationUserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}