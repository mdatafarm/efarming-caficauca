using EFarming.Common;
using EFarming.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface AdminManager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    public interface IAdminManager<T, E> where T : EntityDTO where E : Entity
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        ICollection<T> GetAll(string[] includes = null);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns></returns>
        ICollection<T> GetAll<KProperty>(Expression<Func<E, KProperty>> orderByExpression);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns></returns>
        ICollection<T> GetAll<KProperty>(Specification<E> filterSpecification, Expression<Func<E, KProperty>> orderByExpression);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="page">The page.</param>
        /// <param name="count">The count.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns></returns>
        ICollection<T> GetAll(T filter, int page, int count, string orderBy, bool ascending = true);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(Guid id);

        /// <summary>
        /// Creates the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns></returns>
        bool Create(T entityDTO);

        /// <summary>
        /// Edits the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns></returns>
        bool Edit(T entityDTO);

        /// <summary>
        /// Removes the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns></returns>
        bool Remove(T entityDTO);
    }
}
