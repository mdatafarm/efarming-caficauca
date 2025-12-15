using EFarming.Core.ComercialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class AgreementConfiguration : BasicConfiguration<Agreement>
    {
        public AgreementConfiguration()
        {
            this.Property(a => a.OurRef);
            this.Property(a => a.Date);
            this.Property(a => a.Volume);
            this.Property(a => a.ShipmentDate);
            this.Property(a => a.Quality);
            this.Property(a => a.LotsNumber);
            this.Property(a => a.PriceDate);
            this.Property(a => a.PriceDifferential);
            this.Property(a => a.Terms);
            this.Property(a => a.Weights);
            this.Property(a => a.Payment);
            this.Property(a => a.Samples);
            this.Property(a => a.Arbitration);
            this.Property(a => a.Others);

            this.ToTable("Agreements");

        }
    }
}
