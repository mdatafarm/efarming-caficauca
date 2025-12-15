using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.TasqModule
{
    public class TasqSubModuleApi
    {
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public int moduleID { get; set; }
        public string moduleName { get; set; }
        public int submoduleID { get; set; }
        public string submoduleName { get; set; }
    }
}
