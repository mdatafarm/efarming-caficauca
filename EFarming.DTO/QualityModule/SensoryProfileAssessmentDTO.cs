using EFarming.Common;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// SensoryProfileAssessmentDTO HistoricalDTO
    /// </summary>
    public class SensoryProfileAssessmentDTO : HistoricalDTO 
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

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
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid? FarmId { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the microlot identifier.
        /// </summary>
        /// <value>
        /// The microlot identifier.
        /// </value>
        public Guid? MicrolotId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public FarmDTO Farm { get; set; }

        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        public InvoiceDTO Invoice { get; set; }

        /// <summary>
        /// Gets or sets the microlot.
        /// </summary>
        /// <value>
        /// The microlot.
        /// </value>
        public MicrolotDTO Microlot { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile answers.
        /// </summary>
        /// <value>
        /// The sensory profile answers.
        /// </value>
        public List<SensoryProfileAnswerDTO> SensoryProfileAnswers { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        public string Total_score { get; set; }
        /// <summary>
        /// Gets the answers by attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public IEnumerable<SensoryProfileAnswerDTO> GetAnswersByAttribute(QualityAttributeDTO attribute)
        {
            var answers = SensoryProfileAnswers.Where(spa => spa.QualityAttributeId.Equals(attribute.Id));
            if (answers == null)
                answers = new List<SensoryProfileAnswerDTO>();
            return answers;
        }
        public string TotalScore(List<EFarming.DTO.QualityModule.QualityAttributeDTO> lq)
        {
            decimal ts = 0;
            foreach (var attr in lq)
            {
                var answer = GetAnswersByAttribute(attr);
                try
                {
                    if (answer != null && answer.First() != null && answer.First().Answer != null)
                    {
                        switch (attr.Description)
                        {
                            case "FRAGANCIA/AROMA": 
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));                          
                                break;
                            case "SABOR":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "SABOR RESIDUAL":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "ACIDEZ":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "CUERPO":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "BALANCE":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "DULZOR":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "PUNTAJE CATADOR":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "TAZA LIMPIA":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                            case "UNIFORMIDAD":
                                    ts = ts + Convert.ToDecimal(answer.First().Answer.Replace(".", ","));
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
               /* var ans = Model.First().GetAnswersByAttribute(attr).GetString();
                var answers = SensoryProfileAnswers.Where(spa => spa.QualityAttributeId.Equals(attribute.Id));
            if (answers == null)
                answers = new List<SensoryProfileAnswerDTO>();*/
            return ts.ToString();
        }

       
    }

    public class SensoryProfileAssessmentDTOAPI : SensoryProfileAssessmentDTO
    { }
}
