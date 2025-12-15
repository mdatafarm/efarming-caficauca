using EFarming.Common;
using EFarming.Common.Logging;
using EFarming.Core;
using EFarming.Core.Specification.Contract;
using EFarming.DAL.Contract;
using EFarming.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EFarming.DAL
{
    /// <summary>
    /// Repository Class
    /// </summary>
    /// <typeparam name="T">Repository</typeparam>
    public class Repository<T> : IRepository<T> where T : Entity
    {
        #region Members

        /// <summary>
        /// The _ unit of work
        /// </summary>
        IQueryableUnitOfWork _UnitOfWork;
        /// <summary>
        /// The _uow
        /// </summary>
        UnitOfWork _uow;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        /// <exception cref="System.ArgumentNullException">unitOfWork</exception>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// Gets the uow.
        /// </summary>
        /// <value>
        /// The uow.
        /// </value>
        public UnitOfWork UOW
        {
            get
            {
                return (UnitOfWork)UnitOfWork;
            }
        }

        /// <summary>
        /// Checks the database.
        /// </summary>
        /// <returns>bool with the state of database</returns>
        public bool CheckDatabase()
        {
            return UOW.IsConnected();
        }

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        public virtual void Add(T item)
        {

            if (item != (T)null)
            {
                item.CreatedAt = DateTime.UtcNow;
                GetSet().Add(item); // add new item in this set
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotAddNullEntity, typeof(T).ToString());

            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item">Item to delete</param>
        public virtual void Destroy(T item)
        {
            if (item != (T)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// Modified item
        /// </summary>
        /// <param name="item">Item to delete</param>
        public virtual void Restore(T item)
        {
            if (item != (T)null)
            {
                item.DeletedAt = null;
                item.UpdatedAt = DateTime.UtcNow;
                _UnitOfWork.SetModified(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// Remove item
        /// </summary>
        /// <param name="item">Item to delete</param>
        public virtual void Remove(T item)
        {
            if (item != (T)null)
            {
                item.DeletedAt = DateTime.UtcNow;
                _UnitOfWork.SetModified(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// Track entity into this repository, really in UnitOfWork.
        /// In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        public virtual void TrackItem(T item)
        {
            if (item != (T)null)
                _UnitOfWork.Attach<T>(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        public virtual void Modify(T item)
        {
            if (item != (T)null)
            {
                item.UpdatedAt = DateTime.UtcNow;
                _UnitOfWork.SetModified(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Message.info_CannotRemoveNullEntity, typeof(T).ToString());
            }
        }

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <param name="includes"></param>
        /// <returns>result</returns>
        public virtual T Get(Guid id, params string[] includes)
        {
            if (id != Guid.Empty)
            {
                var set = GetSet().Where(s => s.Id.Equals(id));
                foreach (var include in includes)
                {
                    set = set.Include(include);
                }
                var result =  set.FirstOrDefault();
                if(result != null)
                {
                    UOW.Refresh(result);
                }
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public virtual IQueryable<T> GetAll(params string[] includes)
        {
            var set = GetSet().Where(e => e.DeletedAt == null);
            
            foreach (var include in includes)
            {
                set = set.Include(include);
            }

            return set;
        }

        /// <summary>
        /// Get all elements of type T that matching a
        /// Specification <paramref name="specification" />
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <param name="includes"></param>
        /// <returns>List of selected elements</returns>
        public virtual IQueryable<T> AllMatching(ISpecification<T> specification, params string[] includes)
        {
            var set = GetSet().Where(e => e.DeletedAt == null);
            foreach (var include in includes)
            {
                set = set.Include(include);
            }
            return set.Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// Gets the filtered and paged.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>List of selected elements</returns>
        public virtual IQueryable<T> GetFilteredAndPaged<KProperty>(System.Linq.Expressions.Expression<Func<T, bool>> filter, int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet().Where(e => e.DeletedAt == null);

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Where(filter)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Where(filter)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>List of selected elements</returns>
        public virtual IQueryable<T> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet().Where(e => e.DeletedAt == null);

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// Get  elements of type T in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public virtual IQueryable<T> GetFiltered(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter).Where(e => e.DeletedAt == null);
        }

        /// <summary>
        /// Sets modified entity into the repository.
        /// When calling Commit() method in UnitOfWork
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        public virtual void Merge(T persisted, T current)
        {
            current.CreatedAt = persisted.CreatedAt;
            current.UpdatedAt = DateTime.UtcNow;
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the set.
        /// </summary>
        /// <returns></returns>
        public IDbSet<T> GetSet()
        {
            return _UnitOfWork.CreateSet<T>();
        }
        #endregion

    }
}
