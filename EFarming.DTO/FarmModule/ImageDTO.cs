using EFarming.Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// ImageDTO EntityDTO
    /// </summary>
    public class ImageDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the thumb.
        /// </summary>
        /// <value>
        /// The name of the thumb.
        /// </value>
        public string ThumbName { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the thumb.
        /// </summary>
        /// <value>
        /// The thumb.
        /// </value>
        public string Thumb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ImageDTO"/> is principal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if principal; otherwise, <c>false</c>.
        /// </value>
        public bool Principal { get; set; }

        /// <summary>
        /// Gets the delete URL.
        /// </summary>
        /// <value>
        /// The delete URL.
        /// </value>
        public string DeleteUrl
        {
            get
            {
                return string.Format("/PhotoGallery/Delete/{0}", Id);
            }
        }

        /// <summary>
        /// Gets the set principal URL.
        /// </summary>
        /// <value>
        /// The set principal URL.
        /// </value>
        public string SetPrincipalUrl
        {
            get
            {
                return string.Format("/PhotoGallery/SetPrincipal/{0}", Id);
            }
        }

        /// <summary>
        /// Gets the type of the delete.
        /// </summary>
        /// <value>
        /// The type of the delete.
        /// </value>
        public string DeleteType
        {
            get
            {
                return "DELETE";
            }
        }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public FarmDTO Farm { get; set; }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns></returns>
        /// 
        public DateTime CreatedAt { get; set; }
        public object ToJSON()
        {
            var create = "";
            if (CreatedAt != DateTime.MinValue)
            {
                create = CreatedAt.ToString("yyyy/MM/dd HH:mm tt");
            }
            else
            {
                create = DateTime.Now.ToString();
            }

            return new { name = Name, createdAt = create, size = Size, url = Url, thumbnailUrl = Thumb, deleteUrl = DeleteUrl, deleteType = DeleteType, setPrincipalUrl = SetPrincipalUrl, principal = Principal };

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ImagesExtension
    {
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="images">The images.</param>
        /// <returns></returns>
        public static object ToJSON(this List<ImageDTO> images)
        {
            List<object> list = new List<object>();
            foreach (var image in images)
            {
                list.Add(image.ToJSON());
            }
            return new { files = list };
        }
    }
}
