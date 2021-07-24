using Application.Common.Enums;
using Application.Common.Models;
using Application.Common.Requests;
using Application.Common.Responses;
using Application.Helper;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TranwallService;

namespace Application.Handlers.Commands
{
    [Serializable()]
    public class DeactivateCardCommand : IRequest<ResponseModel>
    {
        public long CardId { get; set; }
        public string CustomerId { get; set; }

        [XmlAttribute("providerName")]
        public string ProviderName { get; set; }

        [XmlAttribute("msisdn")]
        public long Msisdn { get; set; }
    }

    public class DeactivateCardCommandValidator : AbstractValidator<DeactivateCardCommand>
    {
        public DeactivateCardCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
            RuleFor(c => c.CardId).NotEmpty();
        }
    }

    public class DeactivateCardCommandHandler : IRequestHandler<DeactivateCardCommand, ResponseModel>
    {

        private readonly ICardDbContext _dbContext;
        private readonly IssuerMobileApiClient issuerMobileApi;
        private readonly IConfiguration config;

        public DeactivateCardCommandHandler(ICardDbContext dbContext, IssuerMobileApiClient issuerMobileApi, IConfiguration config)
        {
            _dbContext = dbContext;
            this.issuerMobileApi = issuerMobileApi;
            this.config = config;
        }

        public async Task<ResponseModel> Handle(DeactivateCardCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = new updateCardSettings()
            {
                sessionId = config.GetValue<string>("Tranwall:IssuerApiKey"),
                repositoryName = request.ProviderName,
                msisdn = request.Msisdn,
                cardId = request.CardId,
                cardSettings = new cardSettingsUpdate
                {
                    masterStatus = timedPermissionStatus.OFF
                }  
            };

            var card = await _dbContext.CardBlockers.FirstOrDefaultAsync(c => c.CardId.Equals(request.CardId) && c.CustomerId.Equals(request.CustomerId));
            if (card == null)
                return ResponseModel.Success("Card information does not exist.");
           
            card.CardStatus = false;
            var blockerHistory = new CardBlockerHistory
            {
                CardId = request.CardId,
                Channel = card.Channel,
                CardStatus = false
            };
            _dbContext.CardBlockerHistorys.Add(blockerHistory);
            await _dbContext.SaveChangesAsync();

            return ResponseModel.Success($"Card deactivated successfully for {card.Channel} channel");
        }
    }
}
