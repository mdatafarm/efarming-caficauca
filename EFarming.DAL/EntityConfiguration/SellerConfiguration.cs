using EFarming.Core.ComercialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class SellerConfiguration : BasicConfiguration<Seller>
    {
        public SellerConfiguration()
        {
            this.Property(s => s.Name);
            this.Property(s => s.Footer);
            this.Property(s => s.Header);
            this.Property(s => s.SubHeader);

            this.ToTable("sellers");

        }
    }
}
