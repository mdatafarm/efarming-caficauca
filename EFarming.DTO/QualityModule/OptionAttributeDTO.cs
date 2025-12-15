using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// OpctionAttributeDTO EntityDTO
    /// </summary>
    public class OptionAttributeDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute identifier.
        /// </summary>
        /// <value>
        /// The quality attribute identifier.
        /// </value>
        [Required]
        public Guid QualityAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttributeDTO QualityAttribute { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class OptionAttributeExtensions
    {
        /// <summary>
        /// To the HTML.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static string ToHTML(this List<OptionAttributeDTO> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(QualityAttributeTypes.OPTIONS);
            sb.Append("<br />");
            sb.Append(string.Format(QualityMessage.Options, list.Count));
            return sb.ToString();
        }
    }
}
