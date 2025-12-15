using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public class ReferenceRelationShip : Entity
    {
        [Key, Column(Order = 0)]
        public Guid DocumentId { get; set; }

        [Key, Column(Order = 1)]
        public int DocumentReferenceId { get; set; }

        public string Value { get; set; }

        public virtual DocumentReference DocumentReference { get; set; }
    }
}
