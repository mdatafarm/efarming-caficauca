using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class TASQCriteriaType : EntityWithIntId
    {
        [Required]
        public string QuestionType { get; set; }
        public virtual ICollection<TASQCriteria> TasqCriterias { get; set; }
    }
}
