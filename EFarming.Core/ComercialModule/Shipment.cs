using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public class Shipment : Entity
    {
        [Required]
        public string DocumentBL { get; set; }

        public DateTime ShippingDate { get; set; }

        public string PortOfLanding { get; set; }

        public string PortOfDestination { get; set; }

        public string Vessel { get; set; }

        public string ShippingLine { get; set; }

        [Required]
        public string ExpocafeInvoice { get; set; }

        public virtual ICollection<ContractLot> ContractLots { get; set; }
    }
}
