using Boma.RedeSocial.Domain.Common.Entities;

namespace Boma.RedeSocial.Domain.Subscriptions.Financials
{
    public class FinancialInfo
    {
        public Address Address { get; set;}
        public Document Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        
    }
}
