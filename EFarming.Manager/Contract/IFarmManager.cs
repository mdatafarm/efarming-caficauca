using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DTO.FarmModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// FarmManager Interface
    /// </summary>
    public interface IFarmManager
    {
        /// <summary>
        /// Finds this instance.
        /// </summary>
        /// <returns>List of FarmDTO</returns>
        List<FarmDTO> Find();

        int CountFarms(string Type, Guid? id);

        double TotalArea(string Type, Guid? id);

        /// <summary>
        /// Alls the queryable.
        /// </summary>
        /// <returns>IQuerable of Farm</returns>
        IQueryable<Farm> AllQueryable();

        /// <summary>
        /// Gets all Farms. 
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>ICollection of FarmDTO</returns>
        ICollection<FarmDTO> GetAll<KProperty>(Specification<Farm> filterSpecification, Expression<Func<Farm, KProperty>> orderByExpression);

        /// <summary>
        /// Gets the farm by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        FarmDTO GetFarmByCode(string code);

        /// <summary>
        /// Gets all queryable.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>IQuerable of Farm</returns>
        IQueryable<Farm> GetAllQueryable<KProperty>(Specification<Farm> filterSpecification, Expression<Func<Farm, KProperty>> orderByExpression);

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO Details(Guid id, params string[] includes);

        /// <summary>
        /// Bies the family member identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO ByFamilyMemberIdentification(string identification, params string[] includes);

        /// <summary>
        /// Creates the specified farm dto.
        /// </summary>
        /// <param name="farmDTO">The farm dto.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO Create(FarmDTO farmDTO);

        /// <summary>
        /// Creates the without commit.
        /// </summary>
        /// <param name="farmDTO">The farm dto.</param>
        void CreateWithoutCommit(FarmDTO farmDTO);

        /// <summary>
        /// Edits the specified identifier Farm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmDTO">The farm dto.</param>
        /// <param name="mainController">The main controller.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>bool</returns>
        bool Edit(Guid id, FarmDTO farmDTO, string mainController, params string[] includes);

        /// <summary>
        /// Edits the without commit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmDTO">The farm dto.</param>
        /// <param name="mainController">The main controller.</param>
        void EditWithoutCommit(Guid id, FarmDTO farmDTO, string mainController);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        bool remove(Guid id);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Initializes the list.
        /// </summary>
        /// <returns>Dictionary</returns>
        Dictionary<string, List<string>> InitializeList();

        /// <summary>
        /// Calculates the density.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO CalculateDensity(FarmDTO farm);

        /// <summary>
        /// Calculates the fertilizer.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO CalculateFertilizer(FarmDTO farm);

        /// <summary>
        /// Calculates the productivity.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>FarmDTO</returns>
        FarmDTO CalculateProductivity(FarmDTO farm);

        /// <summary>
        /// Calculates the age.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>FarmDTO</returns>
        //FarmDTO CalculateAge(FarmDTO farm);
    }
}