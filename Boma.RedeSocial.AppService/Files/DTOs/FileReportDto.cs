using System;

namespace Boma.RedeSocial.AppService.Files.DTOs
{
    public class FileReportDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set; }

        public string CreatedAt { get; set; }
    }
}
