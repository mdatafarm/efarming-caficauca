using AutoMapper;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Microlot Manager
    /// </summary>
    public class MicrolotManager : IMicrolotManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IMicrolotRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrolotManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MicrolotManager(IMicrolotRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// IEnumerable MicrolotDTO
        /// </returns>
        public IEnumerable<MicrolotDTO> GetAll()
        {
            return Mapper.Map<List<MicrolotDTO>>(_repository.GetAll());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// MicrolotDTO
        /// </returns>
        public MicrolotDTO Get(Guid id)
        {
            return Mapper.Map<MicrolotDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Adds the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        public void Add(MicrolotDTO microlotDTO)
        {
            var microlot = Mapper.Map<Microlot>(microlotDTO);
            _repository.Add(microlot);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Edits the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        public void Edit(MicrolotDTO microlotDTO)
        {
            var microlot = Mapper.Map<Microlot>(microlotDTO);
            var persisted = _repository.Get(microlot.Id);
            _repository.Merge(persisted, microlot);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Removes the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        public void Remove(MicrolotDTO microlotDTO)
        {
            var microlot = Mapper.Map<Microlot>(microlotDTO);
            _repository.Remove(microlot);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Bies the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// MicrolotDTO
        /// </returns>
        public MicrolotDTO ByCode(string code)
        {
            var microlot = _repository.AllMatching(MicrolotSpecification.ByCode(code)).FirstOrDefault();
            return Mapper.Map<MicrolotDTO>(microlot);
        }
    }
}
