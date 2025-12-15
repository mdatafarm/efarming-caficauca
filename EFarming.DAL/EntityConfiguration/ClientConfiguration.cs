using EFarming.Core.ComercialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class ClientConfiguration : BasicConfiguration<Client>
    {
        public ClientConfiguration()
        {
            this.Property(c => c.Name);
            this.Property(c => c.Address);
            this.Property(c => c.ZipCode);
            this.Property(c => c.Address);
            this.Property(c => c.Country);

            this.ToTable("clients");

        }
    }
}
