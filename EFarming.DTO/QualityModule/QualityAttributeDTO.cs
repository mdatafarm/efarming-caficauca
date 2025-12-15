using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// QualityAttributeDTO EntityDTO
    /// </summary>
    public class QualityAttributeDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAttributeDTO"/> class.
        /// </summary>
        public QualityAttributeDTO()
        {
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of.
        /// </summary>
        /// <value>
        /// The type of.
        /// </value>
        public string TypeOf { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public AssessmentTemplateDTO AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the range attribute.
        /// </summary>
        /// <value>
        /// The range attribute.
        /// </value>
        public RangeAttributeDTO RangeAttribute { get; set; }

        /// <summary>
        /// Gets or sets the option attributes.
        /// </summary>
        /// <value>
        /// The option attributes.
        /// </value>
        public List<OptionAttributeDTO> OptionAttributes { get; set; }

        /// <summary>
        /// Gets or sets the open text attribute.
        /// </summary>
        /// <value>
        /// The open text attribute.
        /// </value>
        public OpenTextAttributeDTO OpenTextAttribute { get; set; }

        /// <summary>
        /// Objects the type.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static object ObjectType(QualityAttributeDTO attribute)
        {
            return attribute;
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <value>
        /// The HTML.
        /// </value>
        public string Html
        {
            get { return ToHTML(); }
        }

        /// <summary>
        /// To the HTML.
        /// </summary>
        /// <returns></returns>
        public string ToHTML()
        {
            if (TypeOf.Equals(QualityAttributeTypes.OPEN_TEXT))
            {
                return Description;
            }
            else if (TypeOf.Equals(QualityAttributeTypes.OPTIONS))
            {
                return OptionAttributes.ToHTML();
            }
            else if (TypeOf.Equals(QualityAttributeTypes.RANGE))
            {
                return RangeAttribute.ToString();
            }
            return string.Empty;
        }
    }
}
