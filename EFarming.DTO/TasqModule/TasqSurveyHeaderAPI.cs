using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.TasqModule
{
    public class TasqSurveyHeaderAPI
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Farm ID is Required")]
        public string Farm { get; set; }

        public string LabelSurvey { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public string UserId { get; set; }

        public string DateSurvey { get; set; }

        [StringLength(500, ErrorMessage = "La Observacion no puede ser mayor a 500 caracteres")]
        public string Observations { get; set; }
        public Guid AssessmentTemplateId { get; set; }
        public Guid SyncOperation { get; set; }
    }
}
