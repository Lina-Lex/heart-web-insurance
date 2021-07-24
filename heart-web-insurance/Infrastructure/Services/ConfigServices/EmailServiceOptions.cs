namespace Infrastructure.Services.ConfigServices
{
    public class EmailServiceOptions
    {
        public const string SendGridServiceSettings = "SendGridServiceSettings";
        public string APIKey { get; set; }
        public string HostAddress { get; set; }
        public string Caption { get; set; }
        public string Sender { get; set; }
        public string ConfirmationMessage { get; set; }
    }
}
