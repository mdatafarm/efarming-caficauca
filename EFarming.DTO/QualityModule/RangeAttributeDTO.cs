using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// RangeAttributeDTO EntityDTO
    /// </summary>
    public class RangeAttributeDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        [Required]
        public double MinVal { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        [Required]
        public double MaxVal { get; set; }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        [Required]
        public double Step { get; set; }

        /// <summary>
        /// Gets the HTML minimum value.
        /// </summary>
        /// <value>
        /// The HTML minimum value.
        /// </value>
        public string HtmlMinVal
        {
            get
            {
                return MinVal.ToString().Replace(",", ".");
            }
        }

        /// <summary>
        /// Gets the HTML maximum value.
        /// </summary>
        /// <value>
        /// The HTML maximum value.
        /// </value>
        public string HtmlMaxVal
        {
            get
            {
                return MaxVal.ToString().Replace(",", ".");
            }
        }

        /// <summary>
        /// Gets the HTML step.
        /// </summary>
        /// <value>
        /// The HTML step.
        /// </value>
        public string HtmlStep
        {
            get
            {
                return Step.ToString().Replace(",", ".");
            }
        }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttributeDTO QualityAttribute { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(QualityAttributeTypes.RANGE);
            sb.Append("<br />");
            sb.Append(string.Format(QualityMessage.Range, MinVal, MaxVal, Step));
            return sb.ToString();
        }
    }
}
