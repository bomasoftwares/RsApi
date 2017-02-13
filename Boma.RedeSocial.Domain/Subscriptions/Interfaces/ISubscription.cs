using Boma.RedeSocial.Domain.Subscriptions.Financials;
using System;

namespace Boma.RedeSocial.Domain.Interfaces.Entities
{
    public interface ISubscription
    {
        Guid UserId { get; set; }
        DateTime TrialStartedAt { get; set; }
        DateTime TrialEndedAt { get; set; }
        FinancialInfo FinancialInfo { get; set; }
    }
}
