using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public class MDType : EntityWithIntId
    {
        [Required]
        public string Name { get; set; }

        [Display(Name ="Client")]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
