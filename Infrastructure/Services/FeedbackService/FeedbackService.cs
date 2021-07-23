using Infrastructure.Services.ConfigServices;
using Infrastructure.Services.Handler;
using Microsoft.Extensions.Options;
using System;

namespace Infrastructure.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackServiceOptions config;

        public FeedbackService(IOptions<FeedbackServiceOptions> options)
        {
            config = options.Value;
        }

        public bool RegisterFeedback(string contact, string message)
        {
            try
            {
                var json = new RequestHandler().Post(
                    config.UrlClient, 
                    $"{config.RegisterPath}" +
                        $"?{config.PhoneParam}{contact}" +
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
