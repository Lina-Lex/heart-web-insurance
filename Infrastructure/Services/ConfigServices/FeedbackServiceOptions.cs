namespace Infrastructure.Services.ConfigServices
{
    public class FeedbackServiceOptions
    {
        public const string FeedbackService = "FeedbackService";
        public string UrlClient { get; set; }
        public string RegisterPath { get; set; }
        public string PhoneParam { get; set; }
        public string MessageParam { get; set; }
    }
}
