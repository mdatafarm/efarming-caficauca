using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class TASQAssessmentAnswer : Entity
    {
        public Guid TASQAssessmentId { get; set; }
        public int CriteriaId { get; set; }
        public string Value { get; set; }
        public virtual TASQAssessment TASQAssessment { get; set; }
        public virtual TASQCriteria Criteria { get; set; }
    }
}
