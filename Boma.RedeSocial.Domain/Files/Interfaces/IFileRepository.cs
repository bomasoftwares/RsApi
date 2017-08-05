using Boma.RedeSocial.Domain.Common.Interfaces;
using Boma.RedeSocial.Domain.Files.Entities;
using System;
using System.Linq;

namespace Boma.RedeSocial.Domain.Files.Interfaces
{
    public interface IFileRepository : ICommonRepository<File>
    {
        IQueryable<File> GetFilesByReferenceId(Guid referenceId);
        IQueryable<File> GetFilesByQuery(string query);
    }
}
