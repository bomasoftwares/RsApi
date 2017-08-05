using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.AppService.Files.Commands
{
    public class UpdateFileCommand
    {
        public Guid FileId { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public Guid? ReferenceId { get; set; }

        public string UpdateUser { get; set; }
    }
}
