namespace Infrastructure.Services.FeedbackService
{
    public interface IFeedbackService
    {
        bool RegisterFeedback(string contact, string message);
    }
}
