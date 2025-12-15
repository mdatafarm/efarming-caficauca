using EFarming.Core;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EFarming.DAL
{
    public class Storage : IStorage
    {
        #region Public Methods
        /// <summary>
        /// Uploads the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string Upload(Guid farmId, Guid imageId, string name, string path)
        {
            CloudBlockBlob blockBlob = Setup(farmId, imageId, name);
            using (var fileStream = File.OpenRead(path)){
                blockBlob.UploadFromStream(fileStream);
            }
            return blockBlob.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Removes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="name">The name.</param>
        public void Remove(Guid farmId, Guid imageId, string name){
            CloudBlockBlob blockBlob = Setup(farmId, imageId, name);
            
            blockBlob.Delete();
        }


        /// <summary>
        /// Guardar archivos en BlobStorage de Azure
        /// </summary>
        /// <param name="contenedor">valores => "backups-app-content"</param>
        /// <param name="nombreArchivo">nombre con el que se guarda el blob</param>
        /// <param name="archivoContenido">archivo en bytes[]</param>
        /// <param name="contentType">archivo en bytes[]</param> 
        /// <returns>URI del archivo almacenado en el blob para futuras referencias</returns>
        public string UploadToBlob(string contenedor, string nombreArchivo, byte[] archivoContenido, string contentType)
        {
            string blobURI = string.Empty;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Crear blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Referencia del contenedor previamente creado
            CloudBlobContainer container = blobClient.GetContainerReference(contenedor);
            // Si no esxiste el contneedor, crearlo
            container.CreateIfNotExists();

            //  Recuperar referencia del blob
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromByteArray(archivoContenido, 0, archivoContenido.Length);

            // Configurar tiempo de expiración del blob y los permisos del mismo
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(175320);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            // Generar la "firma" para el blob
            string sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);

            // Retornar ruta de consumo del blob, incluyendo el token SAS (Shared Access Signatures)
            blobURI = blobURI + blockBlob.Uri;// + sasBlobToken;

            return blobURI;
        }

        /// <summary>
        /// Eliminar archivos guardados en BlobStorage de Azure.
        /// </summary>
        /// <param name="contenedor">valores => "backups-app-content"</param>
        /// <param name="archivoNombre">nombre con el que se guarda el blob (completo, incluyendo extensión)</param>
        /// <returns>true si se borró el archivo, false si no se pudo completar la operación.</returns>
        public bool DeleteFromBlob(string contenedor, string archivoNombre)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

                // Crear blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Referencia del contenedor previamente creado
                CloudBlobContainer container = blobClient.GetContainerReference(contenedor);

                // Obtener referecnia del blob para el nombre de archivo
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(archivoNombre);

                // Borrar el blob.
                blockBlob.Delete();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// return file name from uri
        /// </summary>
        /// <param name="contenedor">container</param>
        /// <param name="uri">uri</param>
        /// <returns>the file name</returns>
        public string RetornaNombreArchivo(string contenedor, string uri)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

                string fileName = string.Empty;
                var backupBlobClient = storageAccount.CreateCloudBlobClient();
                var backupContainer = backupBlobClient.GetContainerReference(contenedor);

                //Gets List of Blobs
                var list = backupContainer.ListBlobs();

                fileName = list.OfType<CloudBlockBlob>().FirstOrDefault(u => u.Uri.Equals(uri)).Name;



                return fileName;
            }
            catch (Exception)
            {
                return string.Empty;
                throw;
            }
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose(){}
        
        #endregion

        #region Private Methods
        /// <summary>
        /// Setups the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static CloudBlockBlob Setup(Guid farmId, Guid imageId, string name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(farmId.ToString());
            blobContainer.CreateIfNotExists();
            blobContainer.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(string.Format("{0}.{1}", imageId.ToString(), name));

            return blockBlob;
        }
        #endregion
    }
}
