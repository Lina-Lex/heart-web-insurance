using System.Text.Json.Serialization;

namespace HeartInsurance.Web.DTOs.Responses
{
    public class SignUpResponse : ServiceResponse
    {
        [JsonPropertyName("data")]
        public SignUpDataResponse Data { get; set; }
    }
}
