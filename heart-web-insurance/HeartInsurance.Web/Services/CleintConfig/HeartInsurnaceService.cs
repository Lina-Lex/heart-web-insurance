namespace HeartInsurance.Web.Services.CleintConfig
{
    public class HeartInsurnaceService
    {
        public string ClientName { get; set; }
        public string BaseAddressUrl { get; set; }
        public string SignUpEndpoint { get; set; }
        public string SignInEndpoint { get; set; }
        public string PatientEndpoint { get; set; }
        public string EmailConfirmationEndpoint { get; set; }
        public string PassCodeValidationEndpoint { get; set; }
        public string ClientTimeOut { get; set; }
    }
}
