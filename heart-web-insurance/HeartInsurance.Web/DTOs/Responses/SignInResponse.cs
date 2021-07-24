using System.Text.Json.Serialization;

namespace HeartInsurance.Web.DTOs.Responses
{
    public class SignInResponse : ServiceResponse
    {
        [JsonPropertyName("data")]
        public BaseDataResponse Data { get; set; }
    }
}