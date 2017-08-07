using Boma.RedeSocial.Api.Controllers.Helpers;
using Boma.RedeSocial.AppService.Files;
using Boma.RedeSocial.AppService.Files.Commands;
using Boma.RedeSocial.AppService.Files.DTOs;
using Boma.RedeSocial.AppService.Users.Services;
using Boma.RedeSocial.Crosscut.AssertConcern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Boma.RedeSocial.Api.Controllers
{
    [Authorize]
    public class FileController : ApiController
    {
        public FileController()
        {
            FileAppService = new FileAppService();
            UserAppService = new UserAppService();
        }

        private FileAppService FileAppService { get; set; }
        private UserAppService UserAppService { get; set; }

        #region Files

        [HttpPost]
        [Route("files")]
        public void UploadNewFile()
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");

            foreach (var file in this.GetPostedFiles())
            {

                FileAppService.Upload(new AddFileCommand()
                {
                    FileName = file.FileName,
                    File = file.Content,
                    ContentType = file.ContentType,
                    CreateUser = user.UserName,
                    ReferenceId = Guid.Parse(user.Id)
                });
            }
            
        }

        [HttpPut]
        [Route("files/{fileId:guid}")]
        public void UpdatePhoto([FromUri] Guid fileId)
        {
            var files = this.GetPostedFiles();
            AssertConcern.AssertArgumentTrue(files.Count() > 0, "Informe o arquivo");
            var file = files.First();

            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");

            FileAppService.Update(new UpdateFileCommand()
            {
                FileId = fileId,
                FileName = file.FileName,
                File = file.Content,
                ContentType = file.ContentType,
                UpdateUser = User.Identity.Name,
                ReferenceId = user.UserId
            });
        }

        [HttpDelete]
        [Route("files/{fileId:guid}")]
        public void RemoveFile([FromUri] Guid fileId)
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");

            FileAppService.Remove(fileId, User.Identity.Name);
        }

        [HttpGet]
        [Route("files/{referenceId:guid}")]
        public IEnumerable<FileDto> GetFiles([FromUri] Guid referenceId)
        {
            return FileAppService.GetFiles(new GetFilesCommand() { ReferenceId = referenceId });
        }

        [HttpGet]
        [Route("files/search")]
        public IEnumerable<FileDto> SearchFiles([FromUri] string query)
            => FileAppService.SearchFiles(new SearchFilesCommand() { Query = query });

        [HttpGet]
        [Route("files/{fileId:guid}/download")]
        public FileDto DownloadFile([FromUri] Guid fileId) => FileAppService.DownloadFile(fileId);

        #endregion

        #region Photos

        [HttpGet]
        [Route("files/photos")]
        public IEnumerable<FileDto> GetPhotos()
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");

            return FileAppService.GetPhotos(new GetFilesCommand() { ReferenceId = user.UserId, ContentType = "image" });
        }


        #endregion

        #region Videos

        [HttpGet]
        [Route("files/{referenceId:guid}/videos")]
        public IEnumerable<FileDto> GetVideos([FromUri] Guid referenceId)
        {
            return FileAppService.GetFiles(new GetFilesCommand() { ReferenceId = referenceId, ContentType = "video" });
        }
        #endregion

        
}
}
