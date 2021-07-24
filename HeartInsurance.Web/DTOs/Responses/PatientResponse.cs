using System.Text.Json.Serialization;

namespace HeartInsurance.Web.DTOs.Responses
{
    public class PatientResponse : ServiceResponse
    {
        [JsonPropertyName("data")]
        public PatientDataResponse Data { get; set; }
    }

    public class PatientDataResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
    }
}
