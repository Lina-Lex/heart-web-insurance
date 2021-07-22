using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Interfaces.Application;
using FluentValidation;
using Infrastructure.Services.FeedbackService;
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

        [Required]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string Token { get; private set; }

        public void SetToken(string token)
        {
            Token = token;
        }
    }

    public class FeedbackCommandHandler : IRequestHandler<RegisterFeedbackCommand, ResponseModel>
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackCommandHandler(IFeedbackService feedbackService)
        {
            this._feedbackService = feedbackService;
        }
        public async Task<ResponseModel> Handle(RegisterFeedbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request != null)
                {
                    _feedbackService.RegisterFeedback(request.PhoneNumber, request.PhoneNumber);
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
