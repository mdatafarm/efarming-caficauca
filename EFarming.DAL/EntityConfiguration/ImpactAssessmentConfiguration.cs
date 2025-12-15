using EFarming.Core.ImpactModule.ImpactAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class ImpactAssessmentConfiguration : BasicConfiguration<ImpactAssessment>
    {
        public ImpactAssessmentConfiguration()
        {
            this.Property(ia => ia.Date).IsRequired();
            this.Property(ia => ia.Description).IsRequired().HasMaxLength(128);
            this.HasMany(ia => ia.Answers)
                .WithMany(co => co.ImpactAssessments)
                .Map(m =>
                {
                    m.MapLeftKey("ImpactAssessmentId");
                    m.MapRightKey("CriteriaOptionId");
                    m.ToTable("ImpactAssessmentAnswers");
                });
            this.ToTable("impactAssessments");
        }
    }
}
