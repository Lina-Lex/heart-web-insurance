using System.Text.Json.Serialization;

namespace HeartInsurance.Web.DTOs.Responses
{
    public class ServiceResponse
    {
        [JsonPropertyName("responseStatusCodes")]
        public string ResponseStatusCodes { get; set; }

        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }
}