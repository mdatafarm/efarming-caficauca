using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.TasqModule
{
    public class TasqQuestionAPI
    {
        public int moduleID { get; set; }
        public string moduleName { get; set; }
        public string answer { get; set; }
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public string flag { get; set; }
        public int numQuestion { get; set; }
        public string numText { get; set; }
        public string options { get; set; }
        public int orden { get; set; }
        public int questionID { get; set; }
        public string questionType { get; set; }
        public string required { get; set; }
        public string text { get; set; }
        public string txtInfo { get; set; }
    }
}
