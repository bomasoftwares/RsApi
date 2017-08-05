using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Boma.RedeSocial.Api.Controllers.Helpers
{
    public static class FileHelper
    {
        public static IEnumerable<FileUploaded> GetPostedFiles(this ApiController controller)
        {
            if (!controller.Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var files = HttpContext.Current.Request.Files;

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var buffer = new byte[file.ContentLength];
                file.InputStream.Read(buffer, 0, file.ContentLength);

                yield return new FileUploaded
                {
                    FileName = file.FileName,
                    Content = buffer,
                    ContentType = file.ContentType
                };
            }
        }
    }

    public class FileUploaded
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}