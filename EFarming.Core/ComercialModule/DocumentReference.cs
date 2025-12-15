using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public class DocumentReference : EntityWithIntId
    {
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the reference.
        /// </summary>
        /// <value>
        /// specific type of the reference into the lot information
        /// </value>
        public string RefType { get; set; }

        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
