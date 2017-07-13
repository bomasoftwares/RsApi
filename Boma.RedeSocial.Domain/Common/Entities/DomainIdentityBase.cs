using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Boma.RedeSocial.Domain.Common.Entities
{
    public class DomainIdentityBase: IdentityUser, IUser<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeleteBy { get; set; }
        
    }
}
