using EFarming.Core.QualityModule.SensoryProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class SensoryProfileConfiguration : BasicConfiguration<SensoryProfileAssessment>
    {
        public SensoryProfileConfiguration()
        {
            Property(spa => spa.Type);
            Property(spa => spa.Date).IsRequired();
            Property(spa => spa.Description).IsRequired().HasMaxLength(64);
            
            HasMany(spa => spa.SensoryProfileAnswers)
                .WithRequired(spa => spa.SensoryProfileAssessment)
                .HasForeignKey(spa => spa.SensoryProfileAssessmentId);

            ToTable("sensoryProfileAssessments");
        }
    }

    class SensoryProfileAnswerConfiguration : BasicConfiguration<SensoryProfileAnswer>
    {
        public SensoryProfileAnswerConfiguration()
        {
            Property(spa => spa.Answer);
        }
    }
}
