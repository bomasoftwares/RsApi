using Boma.RedeSocial.Api.Controllers.Helpers;
using Boma.RedeSocial.AppService.Files;
using Boma.RedeSocial.AppService.Files.Commands;
using Boma.RedeSocial.AppService.Files.DTOs;
using Boma.RedeSocial.AppService.Users.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace Boma.RedeSocial.Api.Controllers
{
    public class FileController : ApiController
    {
        public FileController()
        {
            FileAppService = new FileAppService();
            UserAppService = new UserAppService();
        }

        private FileAppService FileAppService { get; set; }
        private UserAppService UserAppService { get; set; }
        
        [HttpPost]
        [Route("files")]
        public void UploadNewFile()
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);

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

        [HttpGet]
        [Route("files/{referenceId:guid}")]
        public IEnumerable<FileDto> GetFiles([FromUri] Guid referenceId)
        {
            return FileAppService.GetFiles(new GetFilesCommand() { ReferenceId = referenceId });
        }

        [HttpGet]
        [Route("files/{referenceId:guid}/photos")]
        public IEnumerable<FileDto> GetPhotos([FromUri] Guid referenceId)
        {
            return FileAppService.GetFiles(new GetFilesCommand() { ReferenceId = referenceId, ContentType = "image" });
        }

        [HttpGet]
        [Route("files/{referenceId:guid}/videos")]
        public IEnumerable<FileDto> GetVideos([FromUri] Guid referenceId)
        {
            return FileAppService.GetFiles(new GetFilesCommand() { ReferenceId = referenceId, ContentType = "video" });
        }

        [HttpGet]
        [Route("files/search")]
        public IEnumerable<FileDto> SearchFiles([FromUri] string query) 
            => FileAppService.SearchFiles(new SearchFilesCommand() { Query = query });

        [HttpGet]
        [Route("files/{fileId:guid}/download")]
        public FileDto DownloadFile([FromUri] Guid fileId) => FileAppService.DownloadFile(fileId);
        
}
}
