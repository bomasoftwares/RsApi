using System;

namespace Boma.RedeSocial.AppService.Files.Commands
{
    public class GetFilesCommand
    {
        public Guid ReferenceId { get; set; }
        public string ContentType { get; set; }
    }
}
