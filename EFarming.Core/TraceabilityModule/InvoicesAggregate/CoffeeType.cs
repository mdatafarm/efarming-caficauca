using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TraceabilityModule.InvoicesAggregate
{
    public class CoffeeType : EntityWithIntId
    {
        public int Identifier { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public decimal AccumulationFactor { get; set; }
    }
}
