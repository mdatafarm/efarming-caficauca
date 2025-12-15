using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Municipality Repository
    /// </summary>
    public class MunicipalityRepository : Repository<Municipality>, IMunicipalityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MunicipalityRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public MunicipalityRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
