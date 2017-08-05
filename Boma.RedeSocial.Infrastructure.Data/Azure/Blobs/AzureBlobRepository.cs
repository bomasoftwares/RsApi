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
    }
}
