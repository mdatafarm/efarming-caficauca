using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FerilizersCalculatorModule
{
    public partial class FertilizerInformation : EntityWithIntId
    {
        public string Name { get; set; }
        public decimal? kg { get; set; }
        public decimal? Price { get; set; }
        public decimal? N { get; set; }
        public decimal? P2O5 { get; set; }
        public decimal? K20 { get; set; }
        public decimal? CaO { get; set; }
        public decimal? MgO { get; set; }
        public decimal? SO4 { get; set; }
        public decimal? B { get; set; }
        public decimal? Zn { get; set; }
        public decimal? Cu { get; set; }
        public decimal? Fe { get; set; }
        public decimal? Mn { get; set; }
        public decimal? Mo { get; set; }
        public decimal? SiO { get; set; }
    }
}
