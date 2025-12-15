using EFarming.Core.FarmModule.FarmAggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFarming.Core.AdminModule.SoilTypeAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FarmConfiguration : BasicConfiguration<Farm>
    {
        public FarmConfiguration()
        {
            this.Property(f => f.Code).HasMaxLength(16).IsRequired();
            this.Property(f => f.Name).HasMaxLength(128).IsRequired();
            this.Property(f => f.CooperativeId).IsOptional();
            this.Property(f => f.FarmSubstatusId).IsOptional();
            this.Property(f => f.GeoLocation).IsOptional();
            this.Property(f => f.OwnershipTypeId).IsOptional();

            HasMany(f => f.Checklists)
                .WithRequired(c => c.Farm)
                .HasForeignKey(c => c.FarmId);

            HasMany(f => f.Invoices)
                .WithRequired(i => i.Farm)
                .HasForeignKey(i => i.FarmId);

            this.HasMany<SoilType>(f => f.SoilTypes)
                .WithMany(st => st.Farms)
                .Map(f =>
                {
                    f.MapLeftKey("FarmId");
                    f.MapRightKey("SoilTypeId");
                    f.ToTable("FarmSoilTypes");
                });

            HasMany(f => f.AssociatedPeople)
                .WithMany(u => u.Farms)
                .Map(f =>
                {
                    f.MapLeftKey("FarmId");
                    f.MapRightKey("UserId");
                    f.ToTable("farmAssociatedPeople");
                });

            HasMany(f => f.Projects)
                .WithMany(p => p.Farms)
                .Map(f =>
                {
                    f.MapLeftKey("FarmId");
                    f.MapRightKey("ProjectId");
                    f.ToTable("FarmProjects");
                });

            this.ToTable("farms");
        }
    }
}
