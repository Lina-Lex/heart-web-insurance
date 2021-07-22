using Newtonsoft.Json;

namespace HeartInsuranceWeb.DTOs.Requests
{
    public class CreateAccountDTORequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }
    }
}
