using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Interfaces.Entities;
using Boma.RedeSocial.Domain.Subscriptions.Financials;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Subscriptions
{
    public class Subscription: DomainEntityBase, ISubscription
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime TrialStartedAt { get; set; }
        public DateTime TrialEndedAt { get; set; }

        public FinancialInfo FinancialInfo { get; set; }

        public Subscription(Guid userId, FinancialInfo financialInfo)
        {
            UserId = userId;
            FinancialInfo = financialInfo;

        }
    }
}
