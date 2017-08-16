using System;
using System.Linq;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Domain.Files.Interfaces;
using Dapper;
using System.Text;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Files
{
    public class FileRepository: CommonRepository<File>, IFileRepository
    {
        public FileRepository(SexMoveContext uow)
            :base(uow)
        {
          
        }


        public IQueryable<File> GetFilesByQuery(string query)
        {
            return QueryWithoutDeleted().Where(f => f.Name.Contains(query));
        }

        public IQueryable<File> GetFilesByReferenceId(Guid referenceId)
        {
            return QueryWithoutDeleted().Where(f => f.ReferenceId == referenceId);
        }

        public IQueryable<File> GetLatestFilesReport(string contentType)
        {
            var sB = new StringBuilder();
            sB.AppendLine(" SELECT  ");
            sB.AppendLine(" 		Id,");
            sB.AppendLine(" 		ReferenceId, ");
            sB.AppendLine(" 		[Name], ");
            sB.AppendLine(" 		ContentType, ");
            sB.AppendLine(" 		CreatedAt");
            sB.AppendLine("   FROM Files ");
            sB.AppendLine("  WHERE DeletedAt IS NULL");
            sB.AppendLine("    AND ContentType like '%'+@contentType+'%' ");
            sB.AppendLine("    AND CreatedAt >= (getutcdate()-30)");
            sB.AppendLine(" ORDER BY CreatedAt DESC");

            return Dapper.Query<File>(sB.ToString(), new { @contentType = contentType }).AsQueryable();
            
        }
    }
}
