using System.Collections.Generic;

namespace Application.Common.Responses
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorSource { get; set; }
        public string ReasonCode { get; set; }
        public string Description { get; set; }
        public List<string> Details { get; set; }

        public ErrorResponse()
        {
            Details = new List<string>();
        }
    }
}
