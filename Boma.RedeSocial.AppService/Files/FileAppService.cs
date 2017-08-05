using Boma.RedeSocial.AppService.Files.Commands;
using Boma.RedeSocial.Crosscut.AssertConcern;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Data.Azure;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Files;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boma.RedeSocial.AppService.Files.DTOs;
using Boma.RedeSocial.AppService.Files.Adapters;

namespace Boma.RedeSocial.AppService.Files
{
    public class FileAppService
    {
        public FileAppService()
        {
            Uow = new SexMoveContext();
            AzureContainer = new AzureContainer();
            FileRepository = new FileRepository(Uow);
            UserRepository = new UserRepository(Uow);
        }

        private readonly SexMoveContext Uow;
        private readonly FileRepository FileRepository;
        private readonly AzureContainer AzureContainer;
        private readonly UserRepository UserRepository;
        public void Upload(AddFileCommand command)
        {
            AssertConcern.AssertArgumentNotEmpty(command.FileName, "Nome do arquivo não pode ser nulo");

            var file = new File(command.FileName, command.ContentType)
            {
                Size = command.File.Length,
                CreatedAt = DateTime.UtcNow,
                CreateBy = command.CreateUser,
                UpdatedAt = DateTime.UtcNow,
                UpdateBy = command.CreateUser,
                ReferenceId = command.ReferenceId.Value
            };
            
            file.GenerateNewId();

            FileRepository.Save(file,command.CreateUser);
            AzureContainer.AddFile(file.Id, command.File);

            Uow.Commit();
        }

        public FileDto DownloadFile(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileDto> GetFiles(GetFilesCommand command)
        {
            if (command.ReferenceId == Guid.Empty)
                return null;

            var files = FileRepository.GetFilesByReferenceId(command.ReferenceId);

            if (!string.IsNullOrWhiteSpace(command.ContentType))
                return files.Where(f => f.ContentType.Contains(command.ContentType)).Select(FileAdapter.ToFileDto).AsEnumerable();

            return files.Select(FileAdapter.ToFileDto).AsEnumerable();

        }

        public IEnumerable<FileDto> SearchFiles(SearchFilesCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Query))
                return null; 

            return FileRepository.GetFilesByQuery(command.Query).Select(FileAdapter.ToFileDto).AsEnumerable();
        }
        
    }
}
