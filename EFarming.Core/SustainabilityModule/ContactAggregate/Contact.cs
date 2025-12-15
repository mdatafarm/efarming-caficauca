using EFarming.Common;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.SustainabilityModule.ContactAggregate
{
    /// <summary>
    /// Contact Entity
    /// </summary>
    public class Contact : Entity
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int TypeId { get; set; }

        public int LocationId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public virtual ContactType Type { get; set; }

        public virtual Location Location { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Farm> Farms { get; set; }
    }
}