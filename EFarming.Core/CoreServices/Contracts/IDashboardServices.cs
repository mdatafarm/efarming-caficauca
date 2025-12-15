using System;
using Newtonsoft;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFarming.Core.DashboardModule;

namespace EFarming.Core.CoreServices.Contracts
{
    /// <summary>
    /// DashboardService Interface
    /// </summary>
    public interface IDashboardServices
    {
        #region General
        /// <summary>
        /// Farmses the by location.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> FarmsByLocation();

        /// <summary>
        /// Plantationses the by location.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> PlantationsByLocation();

        /// <summary>
        /// Impacts the by location.
        /// </summary>
        /// <returns></returns>
        object ImpactByLocation();

        /// <summary>
        /// Tracks the impact by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        object TrackImpactByLocation(int year);

        /// <summary>
        /// Qualities the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        BarChart QualityByLocation(int year);
        #endregion

        #region Department
        /// <summary>
        /// Farmses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Dictionary<string, int> FarmsByDepartment(Guid id);

        /// <summary>
        /// Plantationses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Dictionary<string, int> PlantationsByDepartment(Guid id);

        /// <summary>
        /// Impacts the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        object ImpactByDepartment(Guid id);

        /// <summary>
        /// Tracks the impact by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        object TrackImpactByDepartment(int year, Guid id);

        /// <summary>
        /// Qualities the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        BarChart QualityByDepartment(int year, Guid id);
        #endregion
    }
}
