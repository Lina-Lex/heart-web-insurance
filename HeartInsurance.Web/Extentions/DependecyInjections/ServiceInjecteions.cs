using HeartInsurance.Web.Services.CleintConfig;
using HeartInsurance.Web.Services.HeartInsuranceMicroservice.Interfaces;
using HeartInsurance.Web.Services.HeartInsuranceMicroservice.Interfaces.Implementations;
using HttpClientHelperSolutions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HeartInsurance.Web.Extentions.DependecyInjections
{
    public static class ServiceInjecteions
    {
        private static HeartInsurnaceService heartInsurnaceService;
        public static IServiceCollection AddServiceInjection(this IServiceCollection services)
        {
            services.AddTransient<IHeartInsuranceService, HeartInsuranceService>();
            services.AddScoped(typeof(ClientHelper));

            return services;
        }
        public static IServiceCollection AddHttpClientFactory(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<HeartInsurnaceService>(opts =>
            Configuration.GetSection(nameof(HeartInsurnaceService))
            .Bind(opts));

            services.Configure<HIServiceAuthorization>(opts =>
            Configuration.GetSection(nameof(HIServiceAuthorization))
            .Bind(opts));

            heartInsurnaceService = Configuration.GetSection(nameof(HeartInsurnaceService)).Get<HeartInsurnaceService>();
            services.AddHttpClient(heartInsurnaceService.ClientName, cfg =>
            {
                cfg.BaseAddress = new Uri(heartInsurnaceService.BaseAddressUrl);
                cfg.Timeout = TimeSpan.FromMinutes(double.Parse(heartInsurnaceService.ClientTimeOut));
            });

            return services;
        }
    }
}
