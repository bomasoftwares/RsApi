using Boma.RedeSocial.Domain.Common.Entities;
using System;

namespace Boma.RedeSocial.Domain.Files.Entities
{
    public class File : DomainEntity
    {
        [Obsolete("Hidratação EntityFramework",true)]
        public File()
        {

        }

        public File(string name, string contentType)
        {
            Name = name;
            ContentType = contentType;
        }

        public Guid ReferenceId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        
    }
}
