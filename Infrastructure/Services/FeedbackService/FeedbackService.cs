using Infrastructure.Config;
using Infrastructure.Handler;
using Infrastructure.Services.ConfigServices;
using System;

namespace Infrastructure.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackConfiguration config;

        public FeedbackService()
        {
            config = FeedbackConfiguration.GetInstance;
        }

        public bool RegisterFeedback(string phoneNumber, string message)
        {
            try
            {
                var json = new RequestHandler().Post(
                    config.UrlClient, 
                    $"{config.RegisterPath}" +
                        $"?{config.PhoneParam}{phoneNumber}" +
                        $"&{config.MessageParam}{message}"
                    );
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
