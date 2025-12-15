using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class Module : EntityWithIntId
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public int ModuleOrder { get; set; }
        public Guid AssessmentTemplateId { get; set; }
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }
        public virtual ICollection<SubModule> SubModule { get; set; }
    }
}
