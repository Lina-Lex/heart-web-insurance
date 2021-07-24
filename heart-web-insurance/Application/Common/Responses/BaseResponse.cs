namespace Application.Common.Responses
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; set; }
        public ErrorResponse Error { get; set; }
        public BaseResponse()
        {
            IsSuccessful = false;
        }
    }
}
