using Application.Common.Responses;
using Infrastructure.Services.ConfigServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace HeartInsurance.API.Filters
{
    public class AuthenticationHeaderFilter : IAsyncActionFilter
    {
        private readonly ServiceAuthorizationOptions serviceConfig;
        public AuthenticationHeaderFilter(IOptions<ServiceAuthorizationOptions> options)
        {
            serviceConfig = options.Value;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue(serviceConfig.AppId, out var appId);
            context.HttpContext.Request.Headers.TryGetValue(serviceConfig.AppKey, out var appKey);

            appId = string.Empty;
            appKey = string.Empty;

            if (string.IsNullOrEmpty(appId))
            {
                UnAuthorizedResponse(context, "Unauthorized | Header Authorization is required");
                return;
            }
            
            await next();
        }

        private void UnAuthorizedResponse(ActionExecutingContext context, string description)
        {
            context.Result = new ObjectResult(new BaseResponse
            {
                Error = new ErrorResponse
                {
                    Description = description
                }
            })
            { StatusCode = (int)HttpStatusCode.Unauthorized };
        }
    }
}
