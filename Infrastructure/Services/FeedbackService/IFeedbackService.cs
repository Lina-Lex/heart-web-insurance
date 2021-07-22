namespace Infrastructure.Services.FeedbackService
{
    public interface IFeedbackService
    {
        bool RegisterFeedback(string phoneNumber, string message);
    }
}
