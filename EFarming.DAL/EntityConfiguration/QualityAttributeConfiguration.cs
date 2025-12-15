using EFarming.Core.QualityModule.SensoryProfileAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class QualityAttributeConfiguration : BasicConfiguration<QualityAttribute>
    {
        public QualityAttributeConfiguration()
        {
            Property(a => a.Description).IsRequired().HasMaxLength(128);
            Property(a => a.TypeOf).IsRequired().HasMaxLength(16);
            Property(a => a.Position).IsRequired();

            HasMany(a => a.OptionAttributes)
                .WithRequired(oa => oa.QualityAttribute);
            HasOptional(a => a.RangeAttribute)
                .WithRequired(ra => ra.QualityAttribute);
            HasMany(qa => qa.SensoryProfileAnswers)
                .WithRequired(spa => spa.QualityAttribute);
            HasOptional(a => a.OpenTextAttribute)
                .WithRequired(ota => ota.QualityAttribute);

            ToTable("qualityAttributes");
        }
    }

    class OptionAttributeConfiguration : BasicConfiguration<OptionAttribute>
    {
        public OptionAttributeConfiguration()
        {
            Property(oa => oa.Description).IsRequired().HasMaxLength(64);
            ToTable("optionAttributes");
        }
    }

    class QualityRecommendationsConfiguration : BasicConfiguration<QualityRecommendations>
    {
        public QualityRecommendationsConfiguration()
        {
            Property(oa => oa.OrderOpcion).IsOptional();
            ToTable("QualityRecommendations");
        }
    }

    class RangeAttributeConfiguration : BasicConfiguration<RangeAttribute>
    {
        public RangeAttributeConfiguration()
        {
            Property(ra => ra.MinVal).IsRequired();
            Property(ra => ra.MaxVal).IsRequired();
            Property(ra => ra.Step).IsRequired();
            ToTable("rangeAttributes");
        }
    }

    class OpenTextAttributeConfiguration : BasicConfiguration<OpenTextAttribute>
    {
        public OpenTextAttributeConfiguration()
        {
            Property(oa => oa.Number).IsOptional();
            ToTable("openTextAttribute");
        }
    }
}
