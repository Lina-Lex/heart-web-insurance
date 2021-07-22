using Newtonsoft.Json;

namespace HeartInsurance.Web.DTOs.Requests
{
    public class LoginDTORequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    
    public class LoginDTOResponse
    {
        [JsonProperty("responseStatusCodes")]
        public string ResponseStatusCodes { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public ResponseData Data { get; set; }
    } 
    
    public class ResponseData
    {
        [JsonProperty("applicationUserId")]
        public string ApplicationUserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}