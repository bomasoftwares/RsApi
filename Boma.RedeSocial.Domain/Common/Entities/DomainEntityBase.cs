using System;

namespace Boma.RedeSocial.Domain.Common.Entities
{
    public abstract class DomainEntity: DomainEntityBase
    {
        public DateTime CreatedAt { get; set; }
        public string CreateBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdateBy { get; set; }
        
        public DateTime? DeletedAt { get; set; }
        public string DeleteBy { get; set; }
    }

    public abstract class DomainEntityBase
    {
        public Guid Id { get; set; }

        public void GenerateNewId() => Id = Guid.NewGuid();
    }
    
}
