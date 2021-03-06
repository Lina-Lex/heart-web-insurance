using Application.Common.Exceptions;
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
    public class EmailConfirmationCommand : IRequest<ResponseModel>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ConfirmationToken { get; set; }
    }
    public class EmailConfirmationCommandValidator : AbstractValidator<EmailConfirmationCommand>
    {
        public EmailConfirmationCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.ConfirmationToken).NotEmpty();
        }
    }
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, ResponseModel>
    {
        private readonly ISystemUserActions systemUser;
        public EmailConfirmationCommandHandler(ISystemUserActions systemUser)
        {
            this.systemUser = systemUser;
        }
        public async Task<ResponseModel> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = await systemUser.EmailConfirmation(request.Email, request.ConfirmationToken);
                    if (result.Status.Equals(true))
                        return ResponseModel.Success("Registration completed | Login now.");

                    return ResponseModel.Failure("Email confirmation failed.");
                }
                return ResponseModel.Failure("Request is empty... Email confirmation not completed.");
            }
            catch (Exception ex) { throw new CustomException($"An error occured with description -> {ex}"); }
        }
    }
}
