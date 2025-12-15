using EFarming.Core.AdminModule.MunicipalityAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class MunicipalityConfiguration : BasicConfiguration<Municipality>
    {
        public MunicipalityConfiguration()
        {
            this.Property(m => m.Name).IsRequired().HasMaxLength(64);
            this.Property(m => m.DepartmentId).IsRequired();
            this.HasRequired(m => m.Department);
            this.HasMany(m => m.Villages);
            this.ToTable("municipalities");
        }
    }
}
