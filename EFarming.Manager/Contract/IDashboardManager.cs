using EFarming.Core.DashboardModule;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.DTO.DashboardModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// DashboardManager Interface
    /// </summary>
    public interface IDashboardManager
    {
        #region General
        /// <summary>
        /// Farmses the by location.
        /// </summary>
        /// <returns></returns>
        object FarmsByLocation();

        /// <summary>
        /// Plantationses the by location.
        /// </summary>
        /// <returns></returns>
        object PlantationsByLocation();

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

        /// <summary>
        /// Gets the plantations.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlantationInformationDTO> GetPlantations();
        #endregion

        #region By Department
        /// <summary>
        /// Farmses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        object FarmsByDepartment(Guid id);

        /// <summary>
        /// Plantationses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        object PlantationsByDepartment(Guid id);

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

        /// <summary>
        /// Gets the plantations by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<PlantationInformationDTO> GetPlantationsByDepartment(Guid id);
        #endregion

        #region overview

        /// <summary>
        /// Gets the grouped farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        List<Farm> GetGroupedFarms(Guid? countryId, Guid? supplierId, Guid? supplierChainId);

        /// <summary>
        /// Overviews the size.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PieChart OverviewSize(Guid? countryId, Guid? supplierId, Guid? supplierChainId);

        /// <summary>
        /// Overviews the variety.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PieChart OverviewVariety(Guid? countryId, Guid? supplierId, Guid? supplierChainId);

        /// <summary>
        /// Overviews the sustainability farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        PolarChart OverviewSustainabilityFarm(Guid farmId);

        /// <summary>
        /// Overviews the workers farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        PieChart OverviewWorkersFarm(Guid farmId);

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PieChart OverviewFarms(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PolarChart OverviewSustainability(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        ColumnChart OverviewInvoicesFarms(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PieChart OverviewWorkers(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        PieChart OverviewOwnership(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Overviews the ownership coop.
        /// </summary>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <returns></returns>
        PieChart OverviewOwnershipCoop(Guid cooperativeId);

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        PieChart OverviewFarms(Guid countryId, Guid supplierId);

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        PolarChart OverviewSustainability(Guid countryId, Guid supplierId);

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        ColumnChart OverviewInvoicesFarms(Guid countryId, Guid supplierId);

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        PieChart OverviewWorkers(Guid countryId, Guid supplierId);

        /// <summary>
        /// Overviews the workers coop.
        /// </summary>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <returns></returns>
        PieChart OverviewWorkersCoop(Guid cooperativeId);

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        PieChart OverviewOwnership(Guid countryId, Guid supplierId);

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        PieChart OverviewFarms(Guid countryId);

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        PolarChart OverviewSustainability(Guid countryId);

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        ColumnChart OverviewInvoicesFarms(Guid countryId);

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        PieChart OverviewWorkers(Guid countryId);

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        PieChart OverviewOwnership(Guid countryId);

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <returns></returns>
        PieChart OverviewFarms();

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <returns></returns>
        PolarChart OverviewSustainability();

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <returns></returns>
        ColumnChart OverviewInvoicesFarms();

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <returns></returns>
        PieChart OverviewWorkers();

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <returns></returns>
        PieChart OverviewOwnership();
        #endregion

        #region evolution
        /// <summary>
        /// Evolutions the farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        LineChart EvolutionFarm(Guid farmId);

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <returns></returns>
        LineChart EvolutionFarms();

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        LineChart EvolutionFarms(Guid countryId);

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        LineChart EvolutionFarms(Guid countryId, Guid supplierId);

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        LineChart EvolutionFarms(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <returns></returns>
        LineChart EvolutionVolume();

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        LineChart EvolutionVolume(Guid countryId);

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        LineChart EvolutionVolume(Guid countryId, Guid supplierId);

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        LineChart EvolutionVolume(Guid countryId, Guid supplierId, Guid supplierChainId);

        /// <summary>
        /// Evolutions the volume farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        LineChart EvolutionVolumeFarm(Guid farmId);
        #endregion
    }
}
