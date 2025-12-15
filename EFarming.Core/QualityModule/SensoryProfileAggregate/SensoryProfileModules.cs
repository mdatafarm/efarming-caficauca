using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// Gets or sets the SensoryProfileModules.
    /// </summary>
    /// <value>
    /// The Name.
    /// </value>
    public class SensoryProfileModules : EntityWithIntId
    {
        /// <summary>
        /// Gets or sets the ModulNameeOrder.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ModuleOrder.
        /// </summary>
        /// <value>
        /// The ModuleOrder.
        /// </value>
        public int? ModuleOrder { get; set; }
        /// <summary>
        /// Gets or sets the assessment template id.
        /// </summary>
        /// <value>
        /// The assessment template id.
        /// </value>
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Option Attribute.
        /// </summary>
        /// <value>
        /// The OptionAttribute.
        /// </value>
        public virtual ICollection<OptionAttribute> OptionAttribute { get; set; }
    }
}
