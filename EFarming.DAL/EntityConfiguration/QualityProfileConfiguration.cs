using EFarming.Core.QualityModule.QualityProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class QualityProfileConfiguration: BasicConfiguration<QualityProfile>
    {
        public QualityProfileConfiguration()
        {
            Property(m => m.Name).IsRequired();

            HasMany(m => m.SupplyChains)
                .WithOptional(sc => sc.QualityProfile)
                .HasForeignKey(sc => sc.QualityProfileId);

            ToTable("qualityProfiles");
        }
    }
}
