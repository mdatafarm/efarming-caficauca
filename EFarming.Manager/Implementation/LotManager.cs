using AutoMapper;
using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Lot Manager
    /// </summary>
    public class LotManager : ILotManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ILotRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="LotManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LotManager(ILotRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// IEnumerable LotDTO
        /// </returns>
        public IEnumerable<LotDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<LotDTO>>(_repository.GetAll());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// LotDTO
        /// </returns>
        public LotDTO Get(Guid id)
        {
            return Mapper.Map<LotDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Adds the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        public void Add(LotDTO lotDTO)
        {
            var lot = Mapper.Map<Lot>(lotDTO);
            _repository.Add(lot);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Edits the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        public void Edit(LotDTO lotDTO)
        {
            var lot = Mapper.Map<Lot>(lotDTO);
            var persisted = _repository.Get(lot.Id);
            _repository.Merge(persisted, lot);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Removes the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        public void Remove(LotDTO lotDTO)
        {
            var lot = _repository.Get(lotDTO.Id);
            _repository.Remove(lot);
            _repository.UnitOfWork.Commit();
        }
    }
}
