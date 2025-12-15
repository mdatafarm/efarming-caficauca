using EFarming.Core.ComercialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class AgentConfiguration : BasicConfiguration<Agent>
    {
        public AgentConfiguration()
        {
            this.Property(a => a.Name);
            this.Property(a => a.Email);
            this.Property(a => a.Phone);

            this.ToTable("Agents");

        }
    }
}