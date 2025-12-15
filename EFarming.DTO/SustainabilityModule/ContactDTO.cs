using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.SustainabilityModule
{
    public class ContactDTO : EntityDTO
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int TypeId { get; set; }

        public int LocationId { get; set; }

        public string ActionType { get; set; }

        public Guid UserId { get; set; }

        public ContactTypeDTO Type { get; set; }
        public LocationDTO Location { get; set; }
        public List<TopicDTO> Topics { get; set; }
    }
}
