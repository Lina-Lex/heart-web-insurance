using System.Text.Json.Serialization;

namespace HeartInsuranceWeb.DTOs.Responses
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
        public bool Message { get; set; }

    }
}