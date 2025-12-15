using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.TasqModule
{
    public class FlagIndicator : EntityWithIntId
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
