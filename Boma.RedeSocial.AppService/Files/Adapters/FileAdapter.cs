using Boma.RedeSocial.AppService.Files.DTOs;
using Boma.RedeSocial.Domain.Files.Entities;
using System;
using System.Linq.Expressions;

namespace Boma.RedeSocial.AppService.Files.Adapters
{
    public static class FileAdapter
    {
        public static Expression<Func<File, FileDto>> ToFileDto
            = f => new FileDto
            {
                Id= f.Id,
                Name = f.Name,
                ContentType = f.ContentType,
                Size = f.Size
            };
    }
}
