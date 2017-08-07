using System;

namespace Boma.RedeSocial.AppService.Files.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
    }
}
