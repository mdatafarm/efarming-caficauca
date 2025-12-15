using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class ImageConfiguration : BasicConfiguration<Image>
    {
        public ImageConfiguration()
        {
            this.Property(i => i.Name).IsRequired();
            this.Property(i => i.Size).IsRequired();
            this.Property(i => i.Url).IsRequired();
            this.Property(i => i.Thumb).IsRequired();
            this.Property(i => i.Principal).IsOptional();
            this.HasRequired(i => i.Farm)
                .WithMany(f => f.Images)
                .HasForeignKey(i => i.FarmId);
            this.ToTable("images");
        }
    }
}
