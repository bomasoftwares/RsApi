using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace Boma.RedeSocial.Infrastructure.Data.Azure.Blobs
{
    public class AzureBlobRepository
    {
        private CloudBlobContainer Container { get; set; }
        public AzureBlobRepository(CloudBlobContainer container)
        {
            Container = container;
        }
      
        public void Create(string blobName, byte[] fileContent)
        {
            var blob = Container.GetBlockBlobReference(blobName);
            var stream = new MemoryStream(fileContent);
            blob.UploadFromStream(stream);
        }

        public string Download(string blobName)
        {
            var blob = Container.GetBlockBlobReference(blobName);
            if (blob == null) return null;
            
            return blob.Uri.ToString();
            //var stream = new MemoryStream();
            //blob.DownloadToStream(stream);
            //var buffer = new byte[stream.Length];
            //var offset = 0;
            //blob.DownloadRangeToByteArray(buffer, offset, null, null);
            //return buffer;
        }
    }
}
