namespace HeartInsuranceWeb.Services.CleintConfig
{
    public class HeartInsurnaceService
    {
        public string ClientName { get; set; }
        public string BaseAddressUrl { get; set; }
        public string SignUpEndpoint { get; set; }
        public string SignInEndpoint { get; set; }
        public string EmailConfirmationEndpoint { get; set; }
        public string PassCodeVerificationEndpoint { get; set; }
    }
}
