using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class SoilAnalysisConfiguration : BasicConfiguration<SoilAnalysis>
    {
        public SoilAnalysisConfiguration()
        {
            this.Property(sa => sa.Date).IsRequired();
            this.Property(sa => sa.Comment).IsRequired();
            this.Property(sa => sa.Depth);
            this.HasRequired(sa => sa.Farm)
                .WithMany(f => f.SoilAnalysis)
                .HasForeignKey(sa => sa.FarmId);
            this.ToTable("soilAnalysis");
        }
    }
}
