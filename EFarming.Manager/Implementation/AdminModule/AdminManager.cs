using AutoMapper;
using EFarming.Common;
using EFarming.Core;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFarming.Core.Specification.Implementation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// Admin Manager
    /// </summary>
    public class AdminManager<T, R, E> : IAdminManager<T, E> where T : EntityDTO where E : Entity where R : IRepository<E>
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IRepository<E> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminManager{T, R, E}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AdminManager(R repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includes">The includes.</param>
        /// <returns>ICollection</returns>
        public virtual ICollection<T> GetAll(string[] includes = null)
        {
            includes = includes ?? new string[0];
            return Mapper.Map<ICollection<T>>(_repository.GetAll(includes).ToList());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <returns>ICollection</returns>
        public ICollection<T> GetAll<KProperty>(Expression<Func<E, KProperty>> orderByExpression)
        {
            var result = _repository.GetAll()
                .OrderBy(orderByExpression);

            return Mapper.Map<ICollection<T>>(result.ToList());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="filterSpecification"></param>
        /// <param name="orderByExpression"></param>
        /// <returns></returns>
        public virtual ICollection<T> GetAll<KProperty>(Specification<E> filterSpecification, Expression<Func<E, KProperty>> orderByExpression)
        {
            var result = _repository
                .AllMatching(filterSpecification)
                .OrderBy(orderByExpression);
            return Mapper.Map<ICollection<T>>(result.ToList());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="page">page</param>
        /// <param name="count">count</param>
        /// <param name="orderBy">order by</param>
        /// <param name="ascending">true or false </param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual ICollection<T> GetAll(T filter, int page, int count, string orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T Get(Guid id)
        {
            return Mapper.Map<T>(_repository.Get(id));
        }

        /// <summary>
        /// Creates the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>bool</returns>
        public virtual bool Create(T entityDTO)
        {
            try
            {
                E entity = Mapper.Map<E>(entityDTO);
                _repository.Add(entity);
                _repository.UnitOfWork.Commit();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>bool</returns>
        public virtual bool Edit(T entityDTO)
        {
            try
            {
                E entity = Mapper.Map<E>(entityDTO);
                E persisted = _repository.Get(entityDTO.Id);
                _repository.Merge(persisted, entity);
                _repository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Removes the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>bool</returns>
        public virtual bool Remove(T entityDTO)
        {
            try
            {
                E persisted = _repository.Get(entityDTO.Id);
                _repository.Remove(persisted);
                _repository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }
    }
}
