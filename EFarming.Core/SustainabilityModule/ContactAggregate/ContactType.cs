using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.SustainabilityModule.ContactAggregate
{
    public class ContactType : EntityWithIntId
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
