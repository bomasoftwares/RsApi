using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Interfaces.Entities;

namespace Boma.RedeSocial.Domain.Files
{
    public abstract class File : DomainFileBase, IFile
    {
        public string Name { get; set; }
    }
}
