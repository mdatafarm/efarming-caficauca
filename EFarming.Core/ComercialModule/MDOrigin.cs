using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public class MDOrigin : EntityWithIntId
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CooperativeId { get; set; }

        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
