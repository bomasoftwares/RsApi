using System;
using System.Linq;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Domain.Files.Interfaces;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Files
{
    public class FileRepository: CommonRepository<File>, IFileRepository
    {
        public FileRepository(SexMoveContext uow)
        {
            Uow = uow;
        }

        public IQueryable<File> GetFilesByQuery(string query)
        {
            return QueryWithoutDeleted().Where(f => f.Name.Contains(query));
        }

        public IQueryable<File> GetFilesByReferenceId(Guid referenceId)
        {
            return QueryWithoutDeleted().Where(f => f.ReferenceId == referenceId);
        }
    }
}
