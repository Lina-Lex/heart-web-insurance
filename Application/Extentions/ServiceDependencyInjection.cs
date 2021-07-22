using Application.Common.Helpers.PasswordHelper;
using Application.Implementations;
using Application.Interfaces.Application;
using DAL.DataContext;
using Domain.Entities;
using Infrastructure.Services.ConfigServices;
using Infrastructure.Services.EmailService;
using Infrastructure.Services.FeedbackService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Application.Extensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services)
        {
            services.AddTransient<ISystemUserActions, SystemUserActions>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IApplicationDbContext, AppDbContext>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient(typeof(IEmailManager<ApplicationUser>), typeof(EmailManager));
            services.AddScoped(typeof(GeneratePassCode));
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddServiceInjections(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailServiceOptions>(opts => 
            config.GetSection(EmailServiceOptions.SendGridServiceSettings)
            .Bind(opts));

            services.Configure<ServiceAuthorizationOptions>(opts =>
            config.GetSection(ServiceAuthorizationOptions.Authorization)
           .Bind(opts));

            services.AddSingleton(config);
            services.AddOptions();
            return services;
        }
        public static IServiceCollection AddServiceAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.RequireHttpsMetadata = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = Configuration.GetValue<bool>("Jwt:ValidateSigningKey"),
                    ValidateIssuer = Configuration.GetValue<bool>("Jwt:ValidateIssuer"),
                    ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = Configuration.GetValue<bool>("Jwt:ValidateAudience"),
                    ValidAudience = Configuration.GetValue<string>("Jwt:Audience"),
                    ValidateLifetime = Configuration.GetValue<bool>("Jwt:ValidateLifeTime"),
                    ClockSkew = TimeSpan.FromMinutes(Configuration.GetValue<int>("Jwt:DateToleranceMinutes"))
                };
            });
            return services;
        }
    }
}
