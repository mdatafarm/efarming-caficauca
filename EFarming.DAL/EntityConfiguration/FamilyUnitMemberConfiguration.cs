using EFarming.Core.FarmModule.FamilyUnitAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FamilyUnitMemberConfiguration : BasicConfiguration<FamilyUnitMember>
    {
        public FamilyUnitMemberConfiguration()
        {
            this.Property(fum => fum.Age).IsOptional();
            this.Property(fum => fum.Education).IsOptional();
            this.Property(fum => fum.FarmId).IsRequired();
            this.Property(fum => fum.FirstName).IsRequired().HasMaxLength(64);
            this.Property(fum => fum.Identification).HasMaxLength(16).IsOptional();
            this.Property(fum => fum.LastName).HasMaxLength(32).IsOptional();
            this.Property(fum => fum.MaritalStatus).HasMaxLength(32).IsOptional();
            this.Property(fum => fum.Relationship).HasMaxLength(32).IsOptional();
            this.Property(fum => fum.IsOwner).IsRequired();
            this.Property(fum => fum.IDProductor).IsOptional();
            this.HasRequired(fum => fum.Farm)
                .WithMany(f => f.FamilyUnitMembers)
                .HasForeignKey(fum => fum.FarmId);
            this.ToTable("familyUnitMembers");
        }
    }
}
