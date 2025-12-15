using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFarming.DTO.ImpactModule;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Repository.ImpactModule;
using AutoMapper;
using EFarming.Common;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Indicator Manager
    /// </summary>
    public class IndicatorManager : IIndicatorManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IIndicatorRepository _repository;
        /// <summary>
        /// The _criteria repository
        /// </summary>
        private ICriteriaRepository _criteriaRepository;
        /// <summary>
        /// The _criteria option repository
        /// </summary>
        private ICriteriaOptionRepository _criteriaOptionRepository;
        /// <summary>
        /// The _category repository
        /// </summary>
        private ICategoryRepository _categoryRepository;
        /// <summary>
        /// The _requirement repository
        /// </summary>
        private IRequirementRepository _requirementRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="criteriaRepository">The criteria repository.</param>
        /// <param name="criteriaOptionRepository">The criteria option repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="requirementRepository">The requirement repository.</param>
        public IndicatorManager(IndicatorRepository repository, 
            CriteriaRepository criteriaRepository,
            CriteriaOptionRepository criteriaOptionRepository,
            CategoryRepository categoryRepository,
            RequirementRepository requirementRepository)
        {
            _repository = repository;
            _criteriaRepository = criteriaRepository;
            _criteriaOptionRepository = criteriaOptionRepository;
            _categoryRepository = categoryRepository;
            _requirementRepository = requirementRepository;
        }

        #region Indicators
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// List of IndicatorDTO
        /// </returns>
        public List<IndicatorDTO> GetAll()
        {
            return Mapper.Map<List<IndicatorDTO>>(_repository.GetAll());
        }

        /// <summary>
        /// Gets all by template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>
        /// List of IndicatorDTO
        /// </returns>
        public List<IndicatorDTO> GetAllByTemplate(Guid templateId)
        {
            return Mapper.Map<List<IndicatorDTO>>(_repository.AllMatching(IndicatorSpecification.ByTemplate(templateId)));
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// IndicatorDTO
        /// </returns>
        public IndicatorDTO Get(Guid id)
        {
            return Mapper.Map<IndicatorDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Creates the specified indicator dto.
        /// </summary>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool Create(IndicatorDTO indicatorDTO)
        {
            try
            {
                var indicator = Mapper.Map<Indicator>(indicatorDTO);
                _repository.Add(indicator);
                _repository.UnitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool Edit(Guid id, IndicatorDTO indicatorDTO)
        {
            try
            {
                var current = Mapper.Map<Indicator>(indicatorDTO);
                var persisted = _repository.Get(indicatorDTO.Id);

                _repository.Merge(persisted, current);
                _repository.UnitOfWork.Commit();

                EditCriteria(current, persisted);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="criteriaId">The criteria identifier.</param>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool Edit(Guid id, Guid criteriaId, IndicatorDTO indicatorDTO)
        {
            try
            {
                var current = Mapper.Map<Indicator>(indicatorDTO).Criteria.First(c => c.Id.Equals(criteriaId));
                var persisted = _criteriaRepository.Get(criteriaId);

                EditCriteriaOptions(current, persisted);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool remove(Guid id)
        {
            try
            {
                var indicator = _repository.Get(id);
                _repository.Remove(indicator);
                _repository.UnitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Categories
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>
        /// List of CategoryDTO
        /// </returns>
        public List<CategoryDTO> GetAllCategories(Guid templateId)
        {
            return Mapper.Map<List<CategoryDTO>>(
                _categoryRepository.AllMatching(CategorySpecification.ByTemplate(templateId)));
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// CategoryDTO
        /// </returns>
        public CategoryDTO GetCategory(Guid id)
        {
            return Mapper.Map<CategoryDTO>(_categoryRepository.Get(id));
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="categoryDTO">The category dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool CreateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = Mapper.Map<Category>(categoryDTO);
                _categoryRepository.Add(category);
                _categoryRepository.UnitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="categoryDTO">The category dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool EditCategory(Guid id, CategoryDTO categoryDTO)
        {
            try
            {
                var current = Mapper.Map<Category>(categoryDTO);
                var persisted = _categoryRepository.Get(categoryDTO.Id);

                _categoryRepository.Merge(persisted, current);
                _categoryRepository.UnitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool RemoveCategory(Guid id)
        {
            try
            {
                var category = _categoryRepository.Get(id);
                _categoryRepository.Remove(category);
                _categoryRepository.UnitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Replaces the category.
        /// </summary>
        /// <param name="oldCategoryDTO">The old category dto.</param>
        /// <param name="newCategoryDTO">The new category dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool ReplaceCategory(CategoryDTO oldCategoryDTO, CategoryDTO newCategoryDTO)
        {
            var result = true;
            foreach (var indicator in oldCategoryDTO.Indicators)
            {
                indicator.CategoryId = newCategoryDTO.Id;
                result = result & Edit(indicator.Id, indicator);
            }
            return result;
        }
        #endregion

        #region Requirements
        /// <summary>
        /// Gets all requirements.
        /// </summary>
        /// <returns>
        /// List of RequirementDTO
        /// </returns>
        public List<RequirementDTO> GetAllRequirements()
        {
            return Mapper.Map<List<RequirementDTO>>(_requirementRepository.GetAll());
        }

        /// <summary>
        /// Gets the requirement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// RequirementDTO
        /// </returns>
        public RequirementDTO GetRequirement(Guid id)
        {
            return Mapper.Map<RequirementDTO>(_requirementRepository.Get(id));
        }
        #endregion

        #region private methods
        /// <summary>
        /// Edits the criteria options.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        private void EditCriteriaOptions(Criteria current, Criteria persisted)
        {
            var added = current.CriteriaOptions.Except(persisted.CriteriaOptions, new EntityComparer<CriteriaOption>());
            var removed = persisted.CriteriaOptions.Except(current.CriteriaOptions, new EntityComparer<CriteriaOption>());
            var edited = current.CriteriaOptions.Except(added, new EntityComparer<CriteriaOption>());

            added.ToList().ForEach(co => _criteriaOptionRepository.Add(co));
            removed.ToList().ForEach(co => _criteriaOptionRepository.Remove(co));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.CriteriaOptions.First(c => c.Id.Equals(item.Id));
                _criteriaOptionRepository.Merge(actual, item);
            }
            _criteriaOptionRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Edits the criteria.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        private void EditCriteria(Indicator current, Indicator persisted)
        {
            var added = current.Criteria.Except(persisted.Criteria, new EntityComparer<Criteria>());
            var removed = persisted.Criteria.Except(current.Criteria, new EntityComparer<Criteria>());
            var edited = current.Criteria.Except(added, new EntityComparer<Criteria>());

            added.ToList().ForEach(c => _criteriaRepository.Add(c));
            removed.ToList().ForEach(c => _criteriaRepository.Remove(c));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.Criteria.First(c => c.Id.Equals(item.Id));
                _criteriaRepository.Merge(actual, item);
            }
            _criteriaRepository.UnitOfWork.Commit();
        }
        #endregion
    }
}
