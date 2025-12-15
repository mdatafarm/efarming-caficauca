using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DAL;
using System;
using System.Linq;

namespace EFarming.Repository.QualityModule
{
    /// <summary>
    /// QualityAttribute Repository
    /// </summary>
    public class QualityAttributeRepository : Repository<QualityAttribute>, IQualityAttributeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAttributeRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public QualityAttributeRepository(UnitOfWork uow) : base(uow) { }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item">Item to delete</param>
        public override void Destroy(QualityAttribute item)
        {
            base.Destroy(item);
            if (item.RangeAttribute != null)
                UOW.Remove(item.RangeAttribute);
            if (item.OpenTextAttribute != null)
                UOW.Remove(item.OpenTextAttribute);
            foreach (var option in item.OptionAttributes)
                UOW.Remove(option);

        }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<QualityAttribute> GetAll(params string[] includes)
        {
            return base.GetAll(includes).OrderBy(qa => qa.Position);
        }

        /// <summary>
        /// Get all elements of type T that matching a
        /// Specification <paramref name="specification" />
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<QualityAttribute> AllMatching(Core.Specification.Contract.ISpecification<QualityAttribute> specification, params string[] includes)
        {
            return base.AllMatching(specification, includes).OrderBy(qa => qa.Position);
        }

        /// <summary>
        /// Updates the attribute.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        public void UpdateAttribute(QualityAttribute current, QualityAttribute persisted)
        {
            if (current.TypeOf.Equals(QualityAttributeTypes.RANGE))            
                UpdateRange(current, persisted);            
            else if (current.TypeOf.Equals(QualityAttributeTypes.OPTIONS))            
                UpdateOptions(current, persisted);            
            else if (current.TypeOf.Equals(QualityAttributeTypes.OPEN_TEXT))            
                UpdateOpenText(current, persisted);            
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="current">The current.</param>
        public void RemoveAttribute(QualityAttribute current)
        {
            if (current.TypeOf.Equals(QualityAttributeTypes.RANGE))            
                RemoveRange(current);            
            else if (current.TypeOf.Equals(QualityAttributeTypes.OPTIONS))            
                RemoveOptions(current);            
            else if (current.TypeOf.Equals(QualityAttributeTypes.OPEN_TEXT))            
                RemoveOpenText(current);            
        }

        #region private Methods
        /// <summary>
        /// Removes the options.
        /// </summary>
        /// <param name="current">The current.</param>
        private void RemoveOptions(QualityAttribute current)
        {
            current.OptionAttributes.ToList().ForEach(p =>
            {
                UOW.Attach(p);
                p.DeletedAt = DateTime.UtcNow;
                UOW.SetModified(p);
            });
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="current">The current.</param>
        private void RemoveRange(QualityAttribute current)
        {
            if (current.RangeAttribute != null)
            {
                UOW.Attach(current.RangeAttribute);
                current.RangeAttribute.DeletedAt = DateTime.UtcNow;
                UOW.SetModified(current.RangeAttribute);
            }
        }

        /// <summary>
        /// Removes the open text.
        /// </summary>
        /// <param name="current">The current.</param>
        private void RemoveOpenText(QualityAttribute current)
        {
            if (current.OpenTextAttribute != null)
            {
                UOW.Attach(current.OpenTextAttribute);
                current.OpenTextAttribute.DeletedAt = DateTime.UtcNow;
                UOW.SetModified(current.OpenTextAttribute);
            }
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateRange(QualityAttribute current, QualityAttribute persisted)
        {
            RemoveOptions(current);
            RemoveOpenText(current);

            if (persisted.RangeAttribute != null)            
                UOW.ApplyCurrentValues(persisted.RangeAttribute, current.RangeAttribute);
            
            else
            {
                current.RangeAttribute.Id = current.Id;
                UOW.Add(current.RangeAttribute);
            }
        }

        /// <summary>
        /// Updates the open text.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateOpenText(QualityAttribute current, QualityAttribute persisted)
        {
            RemoveOptions(current);
            RemoveRange(current);

            if (persisted.OpenTextAttribute != null)            
                UOW.ApplyCurrentValues(persisted.OpenTextAttribute, current.OpenTextAttribute);            
            else
            {
                current.OpenTextAttribute.Id = current.Id;
                UOW.Add(current.OpenTextAttribute);
            }
        }

        /// <summary>
        /// Updates the options.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateOptions(QualityAttribute current, QualityAttribute persisted)
        {
            RemoveRange(current);
            RemoveOpenText(current);

            var added = current.OptionAttributes.Except(persisted.OptionAttributes, new EntityComparer<OptionAttribute>());
            var removed = persisted.OptionAttributes.Except(current.OptionAttributes, new EntityComparer<OptionAttribute>());
            var edited = current.OptionAttributes.Except(added, new EntityComparer<OptionAttribute>());

            added.ToList().ForEach(p => persisted.OptionAttributes.Add(p));
            removed.ToList().ForEach(p => UOW.OptionAttributes.Remove(p));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.OptionAttributes.First(p => p.Id.Equals(item.Id));
                UOW.ApplyCurrentValues(actual, item);
            }
        }
        #endregion
    }
}
