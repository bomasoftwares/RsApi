using Boma.RedeSocial.AppService.Files.DTOs;
using Boma.RedeSocial.Domain.Files.Entities;
using System;
using System.Linq.Expressions;

namespace Boma.RedeSocial.AppService.Files.Adapters
{
    public static class FileAdapter
    {
        public static Expression<Func<File, FileDto>> ToFileDto = f => new FileDto
        {
            Id= f.Id,
            Name = f.Name,
            ContentType = f.ContentType,
            Size = f.Size
        };

        public static Expression<Func<File, FileReportDto>> ToFileReportDto = f => new FileReportDto
        {
            Id = f.Id,
            Name = f.Name,
            ContentType = f.ContentType,
            Size = f.Size,
            CreatedAt = f.CreatedAt.ToShortDateString()
        };

        public static FileDto ToFileDtoWithContent(File file, string content) => new FileDto
        {
            Id = file.Id,
            Name = file.Name,
            ContentType = file.ContentType,
            Size = file.Size,
            Content = content
        };
    }
}
