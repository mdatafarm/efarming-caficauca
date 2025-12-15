using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// FarmSubstatusManager
    /// </summary>
    public class FarmSubstatusManager: AdminManager<FarmSubstatusDTO, FarmSubstatusRepository, FarmSubstatus>, IFarmSubstatusManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IFarmSubstatusRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmSubstatusManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FarmSubstatusManager(FarmSubstatusRepository repository): base(repository)
        {
            _repository = repository;
        }
    }
}
