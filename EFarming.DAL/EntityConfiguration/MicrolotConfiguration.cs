using EFarming.Core.QualityModule.MicrolotAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class MicrolotConfiguration : BasicConfiguration<Microlot>
    {
        public MicrolotConfiguration()
        {
            Property(m => m.Code).IsRequired();

            HasMany(m => m.SensoryProfileAssessments)
                .WithOptional(spa => spa.Microlot)
                .HasForeignKey(spa => spa.MicrolotId);

            ToTable("microlots");
        }
    }
}
