using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.ContactModule
{
    public class ContactNameList
    {
        public IEnumerable<Item> listNames()
        {
            List<Item> Names = new List<Item>();
            Names.Add(new Item
            {
                Text = "Visita a finca",
                Value = "Visita a finca"
            });
            Names.Add(new Item
            {
                Text = "Atención en oficina",
                Value = "Atención en oficina"
            });
            Names.Add(new Item
            {
                Text = "Capacitación",
                Value = "Capacitación"
            });
            return Names;
        }
    }
}
