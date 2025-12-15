using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using EFarming.Core;
using EFarming.DAL;
using System.Configuration;
using System.Web;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Photo Gallery
    /// </summary>
    public class PhotoGalleryController : Controller
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;

        /// <summary>
        /// The _storage
        /// </summary>
        private IStorage _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoGalleryController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PhotoGalleryController(FarmManager manager, Storage storage)
        {
            _manager = manager;
            _storage = storage;
        }

        /// <summary>
        /// Uploads the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>New Json</returns>
        public JsonResult Upload(Guid farmId)
        {
            var farm = _manager.Details(farmId);
            if(farm != null && farm.Images != null && farm.Images.Count() > 0)
                return Json(farm.Images.ToJSON(), JsonRequestBehavior.AllowGet);
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Uploads the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>New Json</returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Upload(FormCollection collection)
        {
            var file = Request.Files[0];
            try
            {
                //string path = Server.MapPath(string.Format("~/Content/Uploads/{0}/original", collection["farmId"]));
                //string thumbPath = Server.MapPath(string.Format("~/Content/Uploads/{0}/thumbnails", collection["farmId"]));

                //Directory.CreateDirectory(path);
                //Directory.CreateDirectory(thumbPath);

                //var filePath = string.Format("{0}/{1}", path, file.FileName);
                //var thumbsPath = string.Format("{0}/thumb-{1}", thumbPath, file.FileName);

                //file.SaveAs(filePath);

                string[] substrings = file.FileName.Split('.');
                string thumbName = "";
                Guid id = Guid.NewGuid();

                string fileName = id.ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));

                var intptr = new IntPtr();
                //Image image = new Bitmap(filePath);

                //thumb.Save(thumbsPath);


                string blobContainerName = ConfigurationManager.AppSettings["StorageFilesContainer"];
                string contentTypeFile = string.Empty;
                byte[] bytesFile;

                MemoryStream target = new MemoryStream();
                ImageConverter converter = new ImageConverter();
                //Cargar al blob el archivo original
                //file.InputStream.CopyTo(target);
                //bytesFile = target.ToArray();

                string uriFile = string.Empty;

                int size = file.ContentLength;

                if (substrings[1] != "pdf")
                {
                    Image image = Image.FromStream(file.InputStream, true, true);
                    Image thumb = image.GetThumbnailImage(64, 64, null, intptr);

                    bytesFile = (byte[])converter.ConvertTo(image, typeof(byte[]));
                    contentTypeFile = MimeMapping.GetMimeMapping(file.FileName);
                    uriFile = _storage.UploadToBlob(blobContainerName, "Caficauca/"+collection["farmId"]+"/" + fileName, bytesFile, contentTypeFile);

                    //Cargar al blob el thumbnail


                    bytesFile = (byte[])converter.ConvertTo(thumb, typeof(byte[]));
                    thumbName = _storage.UploadToBlob(blobContainerName, string.Concat("Caficauca/" + collection["farmId"] + "/"+"thumb-", fileName), bytesFile, contentTypeFile);

                    thumb.Dispose();
                    image.Dispose();
                }
                else
                {

                    bytesFile = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        bytesFile = binaryReader.ReadBytes(file.ContentLength);
                    }
                    contentTypeFile = MimeMapping.GetMimeMapping(file.FileName);
                    uriFile = _storage.UploadToBlob(blobContainerName, "Caficauca/" + collection["farmId"] + "/" + fileName, bytesFile, contentTypeFile);

                    thumbName = "/Images/Adobe_icon_32x32.png";
                }



                //var imageDTO = new ImageDTO
                //{
                //    Id = Guid.NewGuid(),
                //    Name = file.FileName,
                //    ThumbName = string.Concat("thumb-", file.FileName),
                //    Size = file.ContentLength,
                //    Thumb = thumbName,
                //    Url = string.Format("/Content/Uploads/{0}/original/{1}", collection["farmId"], file.FileName),
                //    FarmId = Guid.Parse(collection["farmId"])
                //};

                var imageDTO = new ImageDTO
                {
                    Id = id,
                    Name = fileName,
                    ThumbName = string.Concat("thumb-", fileName),
                    Size = size,
                    Thumb = thumbName,
                    CreatedAt = DateTime.Now,
                    Url = uriFile,
                    FarmId = Guid.Parse(collection["farmId"])
                };

                var farm = _manager.Details(imageDTO.FarmId);
                farm.Images.Add(imageDTO);
                _manager.Edit(imageDTO.FarmId, farm, FarmManager.IMAGES);

                imageDTO = _manager.Details(imageDTO.FarmId).Images.First(i => i.Id.Equals(imageDTO.Id));
                return Json(new { files = new object[] { imageDTO.ToJSON() } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { files = new object[] { new { name = file.FileName, size = file.ContentLength, error = e.Message } } }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Delete(Guid Id, Guid farmId)
        {
            var farm = _manager.Details(farmId);

            var image = farm.Images.First(i => i.Id.Equals(Id));

            string[] substrings = image.Name.Split('.');

            farm.Images.Remove(image);
            _manager.Edit(farmId, farm, FarmManager.IMAGES);

            string blobContainerName = ConfigurationManager.AppSettings["StorageFilesContainer"];


            if (substrings[1] != "pdf")
            {
                //System.IO.File.Delete(Server.MapPath(string.Concat("~", image.Thumb)));
                _storage.DeleteFromBlob(blobContainerName, "Caficauca/" + farmId + "/" + image.ThumbName);
            }

            //System.IO.File.Delete(Server.MapPath(string.Concat("~", image.Url)));
            _storage.DeleteFromBlob(blobContainerName, "Caficauca/" + farmId + "/" + image.Name);

            farm = _manager.Details(farmId);
            return Json(farm.Images.ToJSON(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets the principal.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>Json</returns>
        [HttpPut]
        public JsonResult SetPrincipal(Guid Id, Guid farmId)
        {
            var farm = _manager.Details(farmId);

            var last = farm.Images.FirstOrDefault(i => i.Principal);
            if (last != null)
            {
                last.Principal = false;
                farm.Images.Add(last);
            }

            var image = farm.Images.First(i => i.Id.Equals(Id));
            farm.Images.Remove(image);
            image.Principal = true;
            farm.Images.Add(image);
            _manager.Edit(farmId, farm, FarmManager.IMAGES);

            return Json(farm.Images.ToJSON(), JsonRequestBehavior.AllowGet);
        }
    }
}
