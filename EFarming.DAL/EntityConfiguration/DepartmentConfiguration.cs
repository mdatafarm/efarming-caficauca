using EFarming.Core.AdminModule.DepartmentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class DepartmentConfiguration : BasicConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            this.Property(d => d.Name).HasMaxLength(32).IsRequired();
            this.HasMany(d => d.Municipalities);
            this.ToTable("Departments");
        }
    }
}
