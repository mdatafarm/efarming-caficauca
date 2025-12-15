using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class SubModule : EntityWithIntId
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public int SubModuleOrder { get; set; }
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<TASQCriteria> TASQCriterias { get; set; }
    }
}
