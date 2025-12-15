using AutoMapper;
using EFarming.Core.QualityModule.ChecklistAggregate;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation
{
    public class ChecklistManager : IChecklistManager
    {
        private IChecklistRepository _repository;
        public ChecklistManager(IChecklistRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ChecklistDTO> GetAllByFarm(Guid farmId)
        {
            return Mapper.Map<IEnumerable<ChecklistDTO>>(_repository.AllMatching(ChecklistSpecification.ByFarm(farmId)));
        }

        public ChecklistDTO Get(Guid id)
        {
            return Mapper.Map<ChecklistDTO>(_repository.Get(id));
        }

        public ChecklistDTO Add(ChecklistDTO checklistDTO)
        {
            var checklist = Mapper.Map<Checklist>(checklistDTO);
            _repository.Add(checklist);
            return Mapper.Map<ChecklistDTO>(checklist);
        }

        public bool Edit(ChecklistDTO checklistDTO)
        {
            try
            {
                var checklist = Mapper.Map<Checklist>(checklistDTO);
                var persisted = _repository.Get(checklist.Id);
                _repository.Merge(persisted, checklist);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var toRemove = _repository.Get(id);
                _repository.Remove(toRemove);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
