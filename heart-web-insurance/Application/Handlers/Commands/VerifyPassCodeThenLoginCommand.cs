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
    public class VerifyPassCodeThenLoginCommand : IRequest<ResponseModel>
    {
        [Required]
        public string CodeValue { get; set; }
        [Required]
        public string Email { get; set; }
    }
    public class VerifyPassCodeThenLoginCommandValidator : AbstractValidator<VerifyPassCodeThenLoginCommand>
    {
        public VerifyPassCodeThenLoginCommandValidator()
        {
            RuleFor(c => c.CodeValue).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
        }
    }
    public class VerifyPassCodeThenLoginCommandHandler : IRequestHandler<VerifyPassCodeThenLoginCommand, ResponseModel>
    {
        private readonly ISystemUserActions systemUser;
        public VerifyPassCodeThenLoginCommandHandler(ISystemUserActions systemUser)
        {
            this.systemUser = systemUser;
        }
        public async Task<ResponseModel> Handle(VerifyPassCodeThenLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    var result = await systemUser.VerifyPassCodeThenLogin(request.Email, request.CodeValue);
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
