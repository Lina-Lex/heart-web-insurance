using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Common.Models
{
    public class RequestModel
    {
        [JsonIgnore()]
        [Required]
        public string AppKey { get; set; }

        [JsonIgnore()]
        [Required]
        public string AppId { get; set; }
    }
}
