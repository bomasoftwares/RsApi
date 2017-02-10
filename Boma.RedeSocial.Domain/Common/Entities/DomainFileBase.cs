using System;

namespace Boma.RedeSocial.Domain.Common.Entities
{
    public abstract class DomainFileBase
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
