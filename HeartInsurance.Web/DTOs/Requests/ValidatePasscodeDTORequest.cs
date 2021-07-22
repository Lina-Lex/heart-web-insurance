using Newtonsoft.Json;

namespace HeartInsurance.Web.DTOs.Requests
{
    public class ValidatePasscodeDTORequest
    {
        [JsonProperty("codeValue")]
        public string CodeValue { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
