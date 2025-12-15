using AutoMapper;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// PlantationVariety Manager
    /// </summary>
    public class PlantationVarietyManager : AdminManager<PlantationVarietyDTO, PlantationVarietyRepository, PlantationVariety>, IPlantationVarietyManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IPlantationVarietyRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationVarietyManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PlantationVarietyManager(PlantationVarietyRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<PlantationVarietyAPIDTO> LoadFullData()
        {
            return Mapper.Map<ICollection<PlantationVarietyAPIDTO>>(_repository.GetFullData());
        }
    }
}
