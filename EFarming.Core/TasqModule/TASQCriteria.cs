using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class TASQCriteria : EntityWithIntId
    {
        public string Description { get; set; }
        public int SubModuleId { get; set; }
        public int FlagIndicatorId { get; set; }

        [MaxLength(50)]
        public string Short { get; set; }
        public int CriteriaOrder { get; set; }
        public string Options { get; set; }
        public int CriteriaTypeId { get; set; }
        public virtual TASQCriteriaType CriteriaType { get; set; }
        public virtual SubModule SubModule { get; set; }
        public virtual FlagIndicator FlagIndicator { get; set; }
        public virtual ICollection<TASQAssessmentAnswer> TASQAssessmentAnswers { get; set; }
    }
}
