using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class BasicConfiguration<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public BasicConfiguration()
        {
            //this.HasKey(f => f.Id);
            this.Property(e => e.CreatedAt);
            this.Property(e => e.UpdatedAt);
            this.Property(e => e.DeletedAt);
        }
    }
}
