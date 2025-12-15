using EFarming.Core.AdminModule.AssessmentAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class AssessmentTemplateConfiguration : BasicConfiguration<AssessmentTemplate>
    {
        public AssessmentTemplateConfiguration()
        {
            Property(at => at.Name).IsRequired();
            Property(at => at.Type).IsRequired();

            HasMany(at => at.ImpactAssessments)
                .WithRequired(ia => ia.AssessmentTemplate)
                .HasForeignKey(ia => ia.AssessmentTemplateId);

            HasMany(at => at.SensoryProfileAssessments)
                .WithRequired(spa => spa.AssessmentTemplate)
                .HasForeignKey(spa => spa.AssessmentTemplateId);

            HasMany(at => at.Categories)
                .WithRequired(c => c.AssessmentTemplate)
                .HasForeignKey(c => c.AssessmentTemplateId);

            HasMany(at => at.QualityAttributes)
                .WithRequired(qa => qa.AssessmentTemplate)
                .HasForeignKey(qa => qa.AssessmentTemplateId);

            ToTable("assessmentTemplates");
        }
    }
}
