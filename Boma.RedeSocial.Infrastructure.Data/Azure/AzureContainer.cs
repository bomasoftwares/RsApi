using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Boma.RedeSocial.Infrastructure.Data.Azure.Blobs;
using System.IO;

namespace Boma.RedeSocial.Infrastructure.Data.Azure
{
    public class AzureContainer
    {
        private CloudBlobContainer Container { get; set; }
        private AzureBlobRepository BlobRepository;
        
        private void Connect()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            Container = blobClient.GetContainerReference("files");
            Container.CreateIfNotExists();
            Container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            BlobRepository = new AzureBlobRepository(Container);
        }

        public void AddFile(Guid fileId, byte[] fileContent)
        {
            Connect();
            BlobRepository.Create(fileId.ToString(), fileContent);
        }
    }
}
