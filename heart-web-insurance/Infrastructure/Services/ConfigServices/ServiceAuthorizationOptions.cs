namespace Infrastructure.Services.ConfigServices
{
    public class ServiceAuthorizationOptions
    {
        public const string Authorization = "HeartInsuranceServiceAuthorization";
        public string AppId { get; set; }
        public string AppKey { get; set; }
    }
}