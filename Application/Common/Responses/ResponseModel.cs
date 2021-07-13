using System.Collections.Generic;

namespace Application.Common.Responses
{
    public class ResponseModel : ResponseCodes
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        private readonly ResponseCodes responseCodes = new ResponseCodes();
        public ResponseModel()
        {
            ResponseStatusCodes = ResponseStatusDescription.GetMessage(responseCodes);
        }
        public static ResponseModel Success(string message = null)
        {
            return new ResponseModel()
            {
                Status = true,
                Message = message ?? "Request was Successful"
            };
        }

        public static ResponseModel Failure(string message = null)
        {
            return new ResponseModel()
            {
                Message = message ?? "Request was not completed",
            };
        }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }

        public static ResponseModel<T> Success(T data, string message = null)
        {
            return new ResponseModel<T>()
            {
                Status = true,
                Message = message ?? "Request was Successful",
                Data = data
            };
        }
    }

    public class ResponseErrorModel : ResponseModel
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public static ResponseModel Failure(IDictionary<string, string[]> errors = null, string message = null)
        {
            return new ResponseErrorModel()
            {
                Message = message ?? "Request was not completed",
                Errors = errors ?? new Dictionary<string, string[]>()
            };
        }
    }
}
