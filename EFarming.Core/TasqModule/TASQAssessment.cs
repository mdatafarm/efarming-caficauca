using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class TASQAssessment : Entity
    {
        public DateTime Date { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }
        public Guid FarmId { get; set; }
        public Guid AssessmentTemplateId { get; set; }
        public Guid UserId { get; set; }
        public Guid SyncOperation { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TASQAssessmentAnswer> TASQAssessmentAnswers { get; set; }
    }
}
