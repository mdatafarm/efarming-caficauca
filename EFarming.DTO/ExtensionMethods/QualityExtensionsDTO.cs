using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.ExtensionMethods
{
    /// <summary>
    /// SensoryProfileAssessmentDTOExtensions
    /// </summary>
    public static class SensoryProfileAssessmentDTOExtensions
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="answers">The answers.</param>
        /// <returns>the result</returns>
        public static string GetString(this IEnumerable<SensoryProfileAnswerDTO> answers)
        {
            try
            {
                return answers.Select(ans => ans.Answer).Aggregate((current, next) => string.Format("{0}, {1}", current, next));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
