using Application.Common.Exceptions;
using Application.Common.Models.UserModels;
using Application.Common.Responses;
using Application.Interfaces.Application;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Commands
{
    public class UserSignUpCommand : IRequest<ResponseModel>
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
                    return await systemUser.SignUp(new ApplicationUserModel
                    {
                        Email = request.Email,
                        Name = request.Name,
                        OrganizationName = request.OrganizationName
                    });
                }
                return ResponseModel.Failure("Request is empty... Failed signing-in.");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
