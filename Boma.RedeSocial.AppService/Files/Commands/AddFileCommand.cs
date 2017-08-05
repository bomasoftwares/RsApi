using System;

namespace Boma.RedeSocial.AppService.Files.Commands
{
    public class AddFileCommand
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public Guid? ReferenceId { get; set; }

        public string CreateUser { get; set; }
    }
}
