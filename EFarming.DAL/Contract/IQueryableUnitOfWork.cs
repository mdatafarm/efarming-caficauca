using EFarming.Core;
using System.Data.Entity;

namespace EFarming.DAL.Contract
{

    /// <summary>
    /// The UnitOfWork contract for EF implementation
    /// <remarks>
    /// This contract extend IUnitOfWork for use with EF code
    /// </remarks>
    /// </summary>
    public interface IQueryableUnitOfWork
        : IUnitOfWork, ISql
    {
        /// <summary>
        /// Return a bool indicating that the database is up and running
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// Reload item from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void Refresh<T>(T item) where T : class;
        
        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context, 
        /// the ObjectStateManager, and the underlying store.        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> CreateSet<T>() where T : class;

        /// <summary>
        /// Attach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <param name="item">The item</param>
        void Attach<T>(T item) where T : class;

        /// <summary>
        /// Sets the modified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        void SetModified<T>(T item) where T : class;

        /// <summary>
        /// Apply current values in <paramref name="original"/>
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<T>(T original, T current) where T : class;

    }
}
