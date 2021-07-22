using Newtonsoft.Json;

namespace HeartInsuranceWeb.DTOs.Requests
{
    public class ValidatePasscodeDTORequest
    {
        [JsonProperty("codeValue")]
        public string CodeValue { get; set; }
    }
}
