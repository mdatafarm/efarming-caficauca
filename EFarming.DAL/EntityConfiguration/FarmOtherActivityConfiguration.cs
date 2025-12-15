using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FarmOtherActivityConfiguration : BasicConfiguration<FarmOtherActivity>
    {
        public FarmOtherActivityConfiguration()
        {
            this.Property(foa => foa.Percentage).IsRequired();
            this.HasRequired(foa => foa.Farm)
                .WithMany(f => f.OtherActivities)
                .HasForeignKey(foa => foa.FarmId);
            this.HasRequired(foa => foa.OtherActivity);
            this.ToTable("farmOtherActivities");
        }
    }
}
