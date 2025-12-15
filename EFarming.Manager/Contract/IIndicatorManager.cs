using EFarming.DTO.ImpactModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// IndicatorManager Interface
    /// </summary>
    public interface IIndicatorManager
    {
        #region Indicators
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of IndicatorDTO</returns>
        List<IndicatorDTO> GetAll();

        /// <summary>
        /// Gets all by template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>List of IndicatorDTO</returns>
        List<IndicatorDTO> GetAllByTemplate(Guid templateId);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IndicatorDTO</returns>
        IndicatorDTO Get(Guid id);

        /// <summary>
        /// Creates the specified indicator dto.
        /// </summary>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>bool</returns>
        bool Create(IndicatorDTO indicatorDTO);

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>bool</returns>
        bool Edit(Guid id, IndicatorDTO indicatorDTO);

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="criteriaId">The criteria identifier.</param>
        /// <param name="indicatorDTO">The indicator dto.</param>
        /// <returns>bool</returns>
        bool Edit(Guid id, Guid criteriaId, IndicatorDTO indicatorDTO);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        bool remove(Guid id);
        #endregion

        #region Categories
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>List of CategoryDTO</returns>
        List<CategoryDTO> GetAllCategories(Guid templateId);

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>CategoryDTO</returns>
        CategoryDTO GetCategory(Guid id);

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="categoryDTO">The category dto.</param>
        /// <returns>bool</returns>
        bool CreateCategory(CategoryDTO categoryDTO);

        /// <summary>
        /// Edits the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="categoryDTO">The category dto.</param>
        /// <returns>bool</returns>
        bool EditCategory(Guid id, CategoryDTO categoryDTO);

        /// <summary>
        /// Removes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        bool RemoveCategory(Guid id);

        /// <summary>
        /// Replaces the category.
        /// </summary>
        /// <param name="oldCategoryDTO">The old category dto.</param>
        /// <param name="newCategoryDTO">The new category dto.</param>
        /// <returns>bool</returns>
        bool ReplaceCategory(CategoryDTO oldCategoryDTO, CategoryDTO newCategoryDTO);
        #endregion

        #region Requirements
        /// <summary>
        /// Gets all requirements.
        /// </summary>
        /// <returns>List of RequirementDTO</returns>
        List<RequirementDTO> GetAllRequirements();

        /// <summary>
        /// Gets the requirement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>RequirementDTO</returns>
        RequirementDTO GetRequirement(Guid id);
        #endregion
    }
}
