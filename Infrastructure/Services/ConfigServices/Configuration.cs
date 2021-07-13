using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Infrastructure.Services.ConfigServices
{
    public class Configuration
    {
        protected static IConfiguration ConfigManager;
        private static Configuration _instance = null;
        private Configuration(IConfiguration config) { ConfigManager = config; }
        private Configuration() { }
        public static Configuration GetInstance
        {
            get
            {
                return _instance ?? new Configuration();
            }
        }

        public string SendGridEmailKey = ConfigManager["SendGridService:APIKey"];
        public string SendGridEmailHostAddress = "SendGridService:HostAddress";
        public string SendGridEmailCaption = "SendGridService:Caption";
        public string SendGridSenderName = "SendGridService:Sender";
        public string EmailConfirmationMessage = $"Please kindly confirm your email address";
        //{string.Concat(Enumerable.Repeat(Environment.NewLine, 2))}
    }
}
