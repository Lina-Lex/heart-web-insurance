using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Infrastructure.Config
{
    public class FeedbackConfiguration
    {
        private FeedbackConfiguration(IConfiguration config) 
        { 
            ConfigManager = config; 
        }
        
        protected static IConfiguration ConfigManager;
        
        private static FeedbackConfiguration _instance = null;
        
        private FeedbackConfiguration() { }
        
        public static FeedbackConfiguration GetInstance
        {
            get
            {
                return _instance ?? new FeedbackConfiguration();
            }
        }

        public string UrlClient = ConfigurationManager.AppSettings["FeedbackService:UrlClient"];
        public string RegisterPath = ConfigurationManager.AppSettings["FeedbackService:RegisterPath"];
        public string PhoneParam = ConfigurationManager.AppSettings["FeedbackService:PhoneParam"];
        public string MessageParam = ConfigurationManager.AppSettings["FeedbackService:MessageParam"];
    }
}
