using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.TasqModule
{
    public class TasqAssessmentAPI
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Description { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public Guid idSurvey { get; set; }
        public bool? Public { get; set; }
        public string Title { get; set; }
        public string TypeSurvey { get; set; }
      }
}
