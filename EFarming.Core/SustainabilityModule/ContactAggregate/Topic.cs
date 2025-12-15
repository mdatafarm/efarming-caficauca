using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.SustainabilityModule.ContactAggregate
{
    /// <summary>
    /// Topic Contact Entity
    /// </summary>
    public class Topic : EntityWithIntId
    {
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
