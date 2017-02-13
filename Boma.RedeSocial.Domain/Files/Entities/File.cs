using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Files.Interfaces;

namespace Boma.RedeSocial.Domain.Files.Entities
{
    public abstract class File : DomainFileBase, IFile
    {
        public string Name { get; set; }
    }
}
