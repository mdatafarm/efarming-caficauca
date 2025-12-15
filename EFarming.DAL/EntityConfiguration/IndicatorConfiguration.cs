using EFarming.Core.ImpactModule.IndicatorAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class IndicatorConfiguration : BasicConfiguration<Indicator>
    {
        public IndicatorConfiguration()
        {
            this.Property(i => i.Name).IsRequired().HasMaxLength(64);
            this.Property(i => i.Scale).IsRequired();
            this.Property(i => i.Description).IsOptional();
            this.Property(i => i.CategoryId).IsOptional();
            this.HasMany(i => i.Criteria)
                .WithRequired(c => c.Indicator)
                .HasForeignKey(c => c.IndicatorId);
            this.ToTable("indicators");
        }
    }

    class CriteriaConfiguration : BasicConfiguration<Criteria>
    {
        public CriteriaConfiguration()
        {
            this.Property(c => c.Description).IsRequired();
            this.Property(c => c.Value).IsRequired();
            this.Property(c => c.Mandatory).IsRequired();
            this.HasMany(c => c.CriteriaOptions)
                .WithRequired(co => co.Criteria)
                .HasForeignKey(co => co.CriteriaId);

            this.ToTable("criteria");
        }
    }

    class CriteriaOptionConfiguration : BasicConfiguration<CriteriaOption>
    {
        public CriteriaOptionConfiguration()
        {
            this.Property(co => co.Description).IsRequired().HasMaxLength(256);
            this.Property(co => co.Value).IsRequired();
            this.ToTable("criteriaOptions");
        }
    }

    class CategoryConfiguration : BasicConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.Property(ca => ca.Name).IsRequired().HasMaxLength(128);
            this.Property(ca => ca.Score);
            this.HasMany(ca => ca.Indicators)
                .WithOptional(i => i.Category)
                .HasForeignKey(i => i.CategoryId);
            this.ToTable("categories");
        }
    }

    class RequirementConfiguration : BasicConfiguration<Requirement>
    {
        public RequirementConfiguration()
        {
            Property(r => r.Description).IsRequired();
            HasMany(r => r.Criteria)
                .WithOptional(c => c.Requirement)
                .HasForeignKey(c => c.RequirementId);
            ToTable("requirements");
        }
    }
}
