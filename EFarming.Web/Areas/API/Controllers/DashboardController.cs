using EFarming.Common.SharedClasses;
using EFarming.Core.DashboardModule;
using EFarming.DAL;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Dashboard Controller API
    /// </summary>
    public class DashboardController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();
        /// <summary>
        /// The _manager
        /// </summary>
        private IDashboardManager _manager;
        /// <summary>
        /// The _invoice manager
        /// </summary>
        private IInvoiceManager _invoiceManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="invoiceManager">The invoice manager.</param>
        public DashboardController(DashboardManager manager,InvoiceManager invoiceManager)
        {
            _manager = manager;
            _invoiceManager = invoiceManager;
        }

        #region Dashboard By Location
        #region Location info
        /// <summary>
        /// Farmses the by location.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object FarmsByLocation()
        {
            return _manager.FarmsByLocation();
        }

        /// <summary>
        /// Plantationses the by location.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object PlantationsByLocation()
        {
            return _manager.PlantationsByLocation();
        }
        #endregion

        #region Impact
        /// <summary>
        /// Impacts the by location.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object ImpactByLocation()
        {
            return _manager.ImpactByLocation();
        }

        /// <summary>
        /// Tracks the impact by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet]
        public object TrackImpactByLocation(int year)
        {
            return _manager.TrackImpactByLocation(year);
        }
        #endregion

        #region Quality
        /// <summary>
        /// Qualities the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet]
        public BarChart QualityByLocation(int year)
        {
            return _manager.QualityByLocation(year);
        }
        #endregion

        #region invoices
        /// <summary>
        /// Invoiceses the by farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ColumnChart> InvoicesByFarm(Guid farmId, DateTime? start, DateTime? end, Guid? lotId)
        {
            return _invoiceManager.ChartData(farmId, start, end, lotId);
        }

        /// <summary>
        /// Invoiceses the by location.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ColumnChart> InvoicesByLocation(DateTime? start, DateTime? end, Guid? lotId)
        {
            return _invoiceManager.ChartData(start, end, lotId);
        }
        #endregion
        #endregion

        #region Dashboard By Department
        #region Location info
        /// <summary>
        /// Farmses the by department.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public object FarmsByDepartment(Guid departmentId)
        {
            return _manager.FarmsByDepartment(departmentId);
        }

        /// <summary>
        /// Plantationses the by department.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public object PlantationsByDepartment(Guid departmentId)
        {
            return _manager.PlantationsByDepartment(departmentId);
        }
        #endregion

        #region Impact
        /// <summary>
        /// Impacts the by department.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public object ImpactByDepartment(Guid departmentId)
        {
            return _manager.ImpactByDepartment(departmentId);
        }

        /// <summary>
        /// Tracks the impact by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public object TrackImpactByDepartment(int year, Guid departmentId)
        {
            return _manager.TrackImpactByDepartment(year, departmentId);
        }
        #endregion

        #region Quality
        /// <summary>
        /// Qualities the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public BarChart QualityByDepartment(int year, Guid departmentId)
        {
            return _manager.QualityByDepartment(year, departmentId);
        }
        #endregion
        #endregion

        #region Overview
        /// <summary>
        /// Overviews the size supply chain.
        /// </summary>
        /// <param name="supplyChainId">The supply chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewSizeSupplyChain(Guid supplyChainId)
        {
            return _manager.OverviewSize(null, null, supplyChainId);
        }

        /// <summary>
        /// Overviews the size supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewSizeSupplier(Guid supplierId)
        {
            return _manager.OverviewSize(null, supplierId, null);
        }

        /// <summary>
        /// Overviews the size country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewSizeCountry(Guid countryId)
        {
            return _manager.OverviewSize(countryId, null, null);
        }

        /// <summary>
        /// Overviews the size.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewSize()
        {
            return _manager.OverviewVariety(null, null, null);
        }

        /// <summary>
        /// Overviews the variety supply chain.
        /// </summary>
        /// <param name="supplyChainId">The supply chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewVarietySupplyChain(Guid supplyChainId)
        {
            return _manager.OverviewVariety(null, null, supplyChainId);
        }

        /// <summary>
        /// Overviews the variety supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewVarietySupplier(Guid supplierId)
        {
            return _manager.OverviewVariety(null, supplierId, null);
        }

        /// <summary>
        /// Overviews the variety country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewVarietyCountry(Guid countryId)
        {
            return _manager.OverviewVariety(countryId, null, null);
        }

        /// <summary>
        /// Overviews the variety.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewVariety()
        {
            return _manager.OverviewSize(null, null, null);
        }

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewFarms()
        {
            return _manager.OverviewFarms();
        }

        /// <summary>
        /// Overviews the farms country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewFarmsCountry(Guid countryId)
        {
            return _manager.OverviewFarms(countryId);
        }

        /// <summary>
        /// Overviews the farms supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewFarmsSupplier(Guid supplierId)
        {
            return _manager.OverviewFarms(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Overviews the farms supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewFarmsSupplierChain(Guid supplierChainId)
        {
            return _manager.OverviewFarms(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PolarChart OverviewSustainability()
        {
            return _manager.OverviewSustainability();
        }

        /// <summary>
        /// Overviews the sustainability country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PolarChart OverviewSustainabilityCountry(Guid countryId)
        {
            return _manager.OverviewSustainability(countryId);
        }

        /// <summary>
        /// Overviews the sustainability supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PolarChart OverviewSustainabilitySupplier(Guid supplierId)
        {
            return _manager.OverviewSustainability(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Overviews the sustainability supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PolarChart OverviewSustainabilitySupplierChain(Guid supplierChainId)
        {
            return _manager.OverviewSustainability(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Overviews the volume.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewVolume()
        {
            return _manager.OverviewInvoicesFarms();
        }

        /// <summary>
        /// Overviews the volume country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewVolumeCountry(Guid countryId)
        {
            return _manager.OverviewInvoicesFarms(countryId);
        }

        /// <summary>
        /// Overviews the volume supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewVolumeSupplier(Guid supplierId)
        {
            return _manager.OverviewInvoicesFarms(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Overviews the volume supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewVolumeSupplierChain(Guid supplierChainId)
        {
            return _manager.OverviewInvoicesFarms(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkers()
        {
            return _manager.OverviewWorkers();
        }

        /// <summary>
        /// Overviews the workers country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkersCountry(Guid countryId)
        {
            return _manager.OverviewWorkers(countryId);
        }

        /// <summary>
        /// Overviews the workers supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkersSupplier(Guid supplierId)
        {
            return _manager.OverviewWorkers(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Overviews the workers supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkersSupplierChain(Guid supplierChainId)
        {
            return _manager.OverviewWorkers(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Overviews the workers coop.
        /// </summary>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkersCooperative(Guid cooperativeId)
        {
            return _manager.OverviewWorkersCoop(cooperativeId);
        }

        /// <summary>
        /// Overviews the workers farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewWorkersFarm(Guid farmId)
        {
            return _manager.OverviewWorkersFarm(farmId);
        }

        /// <summary>
        /// Overviews the ownership types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewOwnershipTypes()
        {
            return _manager.OverviewOwnership();
        }

        /// <summary>
        /// Overviews the ownership types country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewOwnershipTypesCountry(Guid countryId)
        {
            return _manager.OverviewOwnership(countryId);
        }

        /// <summary>
        /// Overviews the ownership types supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewOwnershipTypesSupplier(Guid supplierId)
        {
            return _manager.OverviewOwnership(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Overviews the ownership types supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewOwnershipTypesSupplierChain(Guid supplierChainId)
        {
            return _manager.OverviewOwnership(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Overviews the ownership type cooperative.
        /// </summary>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewOwnershipTypeCooperative(Guid cooperativeId)
        {
            return _manager.OverviewOwnershipCoop(cooperativeId);
        }
        #endregion

        #region Evolution
        /// <summary>
        /// Evolutions the sustainability.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionSustainability()
        {
            return _manager.EvolutionFarms();
        }

        /// <summary>
        /// Evolutions the sustainability country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionSustainabilityCountry(Guid countryId)
        {
            return _manager.EvolutionFarms(countryId);
        }

        /// <summary>
        /// Evolutions the sustainability supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionSustainabilitySupplier(Guid supplierId)
        {
            return _manager.EvolutionFarms(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Evolutions the sustainability supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionSustainabilitySupplierChain(Guid supplierChainId)
        {
            return _manager.EvolutionFarms(Guid.Empty, Guid.Empty, supplierChainId);
        }

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionVolume()
        {
            return _manager.EvolutionVolume();
        }

        /// <summary>
        /// Evolutions the volume country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionVolumeCountry(Guid countryId)
        {
            return _manager.EvolutionVolume(countryId);
        }

        /// <summary>
        /// Evolutions the volume supplier.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionVolumeSupplier(Guid supplierId)
        {
            return _manager.EvolutionVolume(Guid.Empty, supplierId);
        }

        /// <summary>
        /// Evolutions the volume supplier chain.
        /// </summary>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart EvolutionVolumeSupplierChain(Guid supplierChainId)
        {
            return _manager.EvolutionVolume(Guid.Empty, Guid.Empty, supplierChainId);
        }
        #endregion

        #region Farm Dashboard
        /// <summary>
        /// Farms the overview.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PolarChart FarmOverview(Guid id)
        {
            return _manager.OverviewSustainabilityFarm(id);
        }

        /// <summary>
        /// Farms the evolution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart FarmEvolution(Guid id)
        {
            return _manager.EvolutionFarm(id);
        }

        /// <summary>
        /// Farms the volume evolution.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public LineChart FarmVolumeEvolution(Guid id)
        {
            return _manager.EvolutionVolumeFarm(id);
        }

        [HttpGet]
        public GraphicLineObject SalesByYear(Guid farmId)
        {
            var GroupedInvoices = db.Invoices
                .Where(f => f.FarmId == farmId)
                .GroupBy(y => y.Date.Year)
                .Select(o => new {
                    Year = o.Key,
                    Value = o.Sum(s => s.Weight)
                });
            GraphicLineObject GraphicObject = new GraphicLineObject();
            GraphicObject.Year = GroupedInvoices.Select(y => y.Year).ToList();
            GraphicObject.Value = GroupedInvoices.Select(v => v.Value).ToList();
            return GraphicObject;
        }

        [HttpGet]
        public GraphicLineObject SalesByHaByYear(Guid farmId)
        {
            var Ha = Convert.ToDouble(db.Productivities.Find(farmId).coffeeArea);
            var GroupedInvoices = db.Invoices
                .Where(f => f.FarmId == farmId)
                .GroupBy(y => y.Date.Year)
                .Select(o => new {
                    Year = o.Key,
                    Value = Math.Round(o.Sum(s => s.Weight)/ Ha, 0)
                });
            GraphicLineObject GraphicObject = new GraphicLineObject();
            GraphicObject.Year = GroupedInvoices.Select(y => y.Year).ToList();
            GraphicObject.Value = GroupedInvoices.Select(v => v.Value).ToList();
            return GraphicObject;
        }

        [HttpGet]
        public GraphicLineObject FertilizersByYear(Guid farmId)
        {
            var GroupedInvoices = db.Fertilizers
                .Where(f => f.FarmId == farmId)
                .GroupBy(y => y.Date.Year)
                .Select(o => new {
                    Year = o.Key,
                    Value = o.Sum(s => s.Quantity) * 1.0
                });
            GraphicLineObject GraphicObject = new GraphicLineObject();
            GraphicObject.Year = GroupedInvoices.Select(y => y.Year).ToList();
            GraphicObject.Value = GroupedInvoices.Select(v => v.Value).ToList();
            return GraphicObject;
        }

        [HttpGet]
        public GraphicLineObject FetilizersByHaByYear(Guid farmId)
        {
            var Ha = Convert.ToDouble(db.Productivities.Find(farmId).coffeeArea);
            var GroupedInvoices = db.Fertilizers
                .Where(f => f.FarmId == farmId)
                .GroupBy(y => y.Date.Year)
                .Select(o => new {
                    Year = o.Key,
                    Value = Math.Round(o.Sum(s => s.Quantity) / Ha, 0)
                });
            GraphicLineObject GraphicObject = new GraphicLineObject();
            GraphicObject.Year = GroupedInvoices.Select(y => y.Year).ToList();
            GraphicObject.Value = GroupedInvoices.Select(v => v.Value).ToList();
            return GraphicObject;
        }
        #endregion

        public class GraphicLineObject
        {
            public List<int> Year { get; set; }
            public List<double> Value { get; set; }
        }
    }
}
