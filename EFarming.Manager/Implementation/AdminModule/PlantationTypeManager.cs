using AutoMapper;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// PlantationType Manager 
    /// </summary>
    public class PlantationTypeManager : AdminManager<PlantationTypeDTO, PlantationTypeRepository, PlantationType>, IPlantationTypeManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IPlantationTypeRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationTypeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PlantationTypeManager(PlantationTypeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<PlantationTypeAPIDTO> LoadFullData()
        {
            return Mapper.Map<ICollection<PlantationTypeAPIDTO>>(_repository.GetFullData());
        }
    }
}
