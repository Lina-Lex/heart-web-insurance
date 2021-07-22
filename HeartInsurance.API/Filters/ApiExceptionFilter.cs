using Application.Common.Exceptions;
using Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace HeartInsurance.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var response = context.Exception is ValidationException
                ? ProcessValidationErrors(context)
                : ProcessSystemErrors(context);
            context.Result = new JsonResult(response);
        }

        private static ResponseModel ProcessSystemErrors(ExceptionContext context)
            => ResponseModel.Failure(context.Exception.Message);

        private ResponseModel ProcessValidationErrors(ExceptionContext context)
        {
            var validationErrors = ((ValidationException)context.Exception).Errors;
            var message = validationErrors.FirstOrDefault().Value.FirstOrDefault();
            return ResponseModel.Failure(message);
        }

    }
}
