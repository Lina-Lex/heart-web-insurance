using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Common.Responses;
using Application.Interfaces.Application;
using FluentValidation;
using Infrastructure.Services.ConfigServices;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Commands
{
    public class UserSignInCommand : IRequest<ResponseModel>
    {
        [Required]
        public string Email { get; set; }
    }
    public class UserSignInCommandValidator : AbstractValidator<UserSignInCommand>
    {
        public UserSignInCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
        }
    }
    public class UserSignInCommandHandler : IRequestHandler<UserSignInCommand, ResponseModel>
    {
        private readonly ISystemUserActions systemUser;
        private readonly ServiceAuthorizationOptions serviceConfig;
        public UserSignInCommandHandler(ISystemUserActions systemUser, IOptions<ServiceAuthorizationOptions> options)
        {
            this.systemUser = systemUser;
            serviceConfig = options.Value;
        }
        public async Task<ResponseModel> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = await systemUser.SignIn(request.Email);
                    if (result.Status.Equals(true))
                        return ResponseModel.Success(result.Message);
                    return ResponseModel.Failure(result.Message);
                }
                return ResponseModel.Failure("Request is empty... Failed signing-in.");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
