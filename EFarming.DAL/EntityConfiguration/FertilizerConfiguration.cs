using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FertilizerConfiguration : BasicConfiguration<Fertilizer>
    {
        public FertilizerConfiguration()
        {
            this.Property(f => f.Name).IsRequired().HasMaxLength(64);
            this.HasRequired(f => f.Farm)
                .WithMany(f => f.Fertilizers)
                .HasForeignKey(f => f.FarmId);
            this.ToTable("fertilizers");
        }
    }
}
