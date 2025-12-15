using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.TasqModule
{
    public class TasqGroupedQuestionsAPI
    {
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string categoryName { get; set; }
        public int NumberQuestions { get; set; }
        public IOrderedEnumerable<TasqQuestionAPI> questions { get; set; }
    }
}
