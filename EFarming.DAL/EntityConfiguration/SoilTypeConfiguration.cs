using EFarming.Core.AdminModule.SoilTypeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class SoilTypeConfiguration : BasicConfiguration<SoilType>
    {
        public SoilTypeConfiguration()
        {
            this.Property(st => st.Name).IsRequired().HasMaxLength(32);
            this.ToTable("SoilTypes");
        }
    }
}
