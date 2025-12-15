using System;

namespace EFarming.Core
{
    /// <summary>
    /// Storage Interface
    /// </summary>
    public interface IStorage : IDisposable
    {
        /// <summary>
        /// Uploads the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string Upload(Guid farmId, Guid imageId, string name, string path);

        /// <summary>
        /// Removes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="name">The name.</param>
        void Remove(Guid farmId, Guid imageId, string name);

        /// <summary>
        /// Save file into blob storage
        /// </summary>
        /// <param name="contenedor">container name</param>
        /// <param name="nombreArchivo">file name</param>
        /// <param name="archivoContenido">bytes of file </param>
        /// <param name="contentType">mime type of file</param>
        /// <returns></returns>
        string UploadToBlob(string contenedor, string nombreArchivo, byte[] archivoContenido, string contentType);

        /// <summary>
        /// Delete file from blob storage
        /// </summary>
        /// <param name="contenedor">container name</param>
        /// <param name="archivoNombre">file name</param>
        /// <returns></returns>
        bool DeleteFromBlob(string contenedor, string archivoNombre);


        /// <summary>
        /// return file name from uri
        /// </summary>
        /// <param name="contenedor">container</param>
        /// <param name="uri">uri</param>
        /// <returns>the file name</returns>
        string RetornaNombreArchivo(string contenedor, string uri);
    }
}
