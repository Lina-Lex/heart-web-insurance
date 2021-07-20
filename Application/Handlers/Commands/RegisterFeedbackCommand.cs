using Application.Common.Exceptions;
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
    public class RegisterFeedbackCommand : IRequest<ResponseModel>
    {
        [Required]
        public string Message { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }

    public class FeedbackCommandValidator : AbstractValidator<RegisterFeedbackCommand>
    {
        public FeedbackCommandValidator()
        {
            RuleFor(c => c.Message).NotEmpty();
            RuleFor(c => c.Token).NotEmpty();
        }
    }

    public class FeedbackCommandHandler : IRequestHandler<RegisterFeedbackCommand, ResponseModel>
    {
        private readonly ISystemUserActions systemUser;
        public FeedbackCommandHandler(ISystemUserActions systemUser)
        {
            this.systemUser = systemUser;
        }

        public async Task<ResponseModel> Handle(RegisterFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {

                }
                return ResponseModel.Failure("Request is empty... Feedback registration failed.");
            }
            catch (Exception ex) 
            { 
                throw new CustomException($"An error occured with description -> {ex.Message}"); 
            }
        }
    }
}
