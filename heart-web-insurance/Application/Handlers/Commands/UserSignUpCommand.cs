using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces.Application;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Commands
{
    public class UserSignUpCommand : RequestModel, IRequest<ResponseModel>
    {
        public string OrganizationName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class UserSignUpCommandValidator : AbstractValidator<UserSignUpCommand>
    {
        public UserSignUpCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
        }
    }
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand, ResponseModel>
    {
        private readonly ISystemUserActions systemUser;
        public UserSignUpCommandHandler(ISystemUserActions systemUser)
        {
            this.systemUser = systemUser;
        }
        public async Task<ResponseModel> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = await systemUser.SignUp(new ApplicationUserModel
                    {
                        Email = request.Email,
                        Name = request.Name,
                        OrganizationName = request.OrganizationName
                    });
                    if (result.Status.Equals(true))
                    {
                        var responseResult = new ApplicationUserResponse
                        {
                            ApplicationUserId = result.Data.ApplicationUserId,
                            Email = result.Data.Email,
                            Name = result.Data.Name,
                            Token = result.Data.Token
                        };
                        
                        return ResponseModel<ApplicationUserResponse>.Success(responseResult, result.Message);
                    }
                       
                    return ResponseModel.Failure(result.Message);
                }
                return ResponseModel.Failure("Request is empty... Failed signing-in.");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
