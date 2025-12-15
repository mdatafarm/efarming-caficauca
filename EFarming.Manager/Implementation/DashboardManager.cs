using EFarming.Common;
using EFarming.Common.Caching;
using EFarming.Common.Resources;
using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.CoreServices.Contracts;
using EFarming.Core.CoreServices.Implementation;
using EFarming.Core.DashboardModule;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.DashboardModule;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// DashBoard Manager
    /// </summary>
    public class DashboardManager : IDashboardManager
    {
        private UnitOfWork db = new UnitOfWork();

        #region CONSTS
        /// <summary>
        /// The evolutio n_ farms
        /// </summary>
        public readonly string EVOLUTION_FARMS = "EVOLUTION_FARMS_";
        #endregion

        /// <summary>
        /// The _services
        /// </summary>
        private IDashboardServices _services;
        /// <summary>
        /// The _farm repository
        /// </summary>
        private IFarmRepository _farmRepository;
        /// <summary>
        /// The _category repository
        /// </summary>
        private ICategoryRepository _categoryRepository;
        /// <summary>
        /// The _assessment repository
        /// </summary>
        private IImpactAssessmentRepository _assessmentRepository;
        /// <summary>
        /// The _criteria option repository
        /// </summary>
        private ICriteriaOptionRepository _criteriaOptionRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardManager"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="farmRepository">The farm repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="assessmentRepository">The assessment repository.</param>
        /// <param name="criteriaOptionRepository">The criteria option repository.</param>
        public DashboardManager(
            DashboardServices services,
            IFarmRepository farmRepository,
            ICategoryRepository categoryRepository,
            IImpactAssessmentRepository assessmentRepository,
            ICriteriaOptionRepository criteriaOptionRepository)
        {
            _services = services;
            _farmRepository = farmRepository;
            _categoryRepository = categoryRepository;
            _assessmentRepository = assessmentRepository;
            _criteriaOptionRepository = criteriaOptionRepository;
        }

        #region dashboard
        /// <summary>
        /// Farmses the by location.
        /// </summary>
        /// <returns></returns>
        public object FarmsByLocation()
        {
            List<List<object>> data = new List<List<object>>();
            foreach (var item in _services.FarmsByLocation())
            {
                data.Add(new List<object> { item.Key, item.Value });
            }
            return new List<object> { new { type = "pie", name = DashboardMessage.Farms_Per_Location, data = data } };
        }


        /// <summary>
        /// Plantationses the by location.
        /// </summary>
        /// <returns></returns>
        public object PlantationsByLocation()
        {
            List<List<object>> data = new List<List<object>>();
            foreach (var item in _services.PlantationsByLocation())
            {
                data.Add(new List<object> { item.Key, item.Value });
            }
            return new List<object> { new { type = "pie", name = DashboardMessage.Plantations_Per_Location, data = data } };
        }


        /// <summary>
        /// Impacts the by location.
        /// </summary>
        /// <returns></returns>
        public object ImpactByLocation()
        {
            return _services.ImpactByLocation();
        }


        /// <summary>
        /// Tracks the impact by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public object TrackImpactByLocation(int year)
        {
            return _services.TrackImpactByLocation(year);
        }


        /// <summary>
        /// Qualities the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public BarChart QualityByLocation(int year)
        {
            return _services.QualityByLocation(year);
        }

        /// <summary>
        /// Gets the plantations.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlantationInformationDTO> GetPlantations()
        {
            return _farmRepository.GetAll()
                .SelectMany(f => f.Productivity.Plantations)
                .GroupBy(p => p.PlantationType)
                .Select(grp => new PlantationInformationDTO
                {
                    Area = grp.Sum(p => Convert.ToDouble(p.Hectares)),
                    Plantation = grp.Key.Name,
                    Varieties = grp.Key.PlantationVarieties.Select(pv => pv.Name)
                });
        }


        /// <summary>
        /// Farmses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object FarmsByDepartment(Guid id)
        {
            List<List<object>> data = new List<List<object>>();
            foreach (var item in _services.FarmsByDepartment(id))
            {
                data.Add(new List<object> { item.Key, item.Value });
            }
            return new List<object> { new { type = "pie", name = DashboardMessage.Farms_Per_Location, data = data } };
        }


        /// <summary>
        /// Plantationses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object PlantationsByDepartment(Guid id)
        {
            List<List<object>> data = new List<List<object>>();
            foreach (var item in _services.PlantationsByDepartment(id))
            {
                data.Add(new List<object> { item.Key, item.Value });
            }
            return new List<object> { new { type = "pie", name = DashboardMessage.Plantations_Per_Location, data = data } };
        }


        /// <summary>
        /// Impacts the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object ImpactByDepartment(Guid id)
        {
            return _services.ImpactByDepartment(id);
        }


        /// <summary>
        /// Tracks the impact by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object TrackImpactByDepartment(int year, Guid id)
        {
            return _services.TrackImpactByDepartment(year, id);
        }


        /// <summary>
        /// Qualities the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BarChart QualityByDepartment(int year, Guid id)
        {
            return _services.QualityByDepartment(year, id);
        }


        /// <summary>
        /// Gets the plantations by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<PlantationInformationDTO> GetPlantationsByDepartment(Guid id)
        {
            return _farmRepository.AllMatching(FarmSpecification.Filter(string.Empty, string.Empty, null, null, null, null, null, id))
                .SelectMany(f => f.Productivity.Plantations)
                .GroupBy(p => p.PlantationType)
                .Select(grp => new PlantationInformationDTO
                {
                    Area = grp.Sum(p => Convert.ToDouble(p.Hectares)),
                    Plantation = grp.Key.Name,
                    Varieties = grp.Key.PlantationVarieties.Select(pv => pv.Name)
                });
        }
        #endregion

        #region Overview
        /// <summary>
        /// Gets the grouped farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public List<Farm> GetGroupedFarms(Guid? countryId, Guid? supplierId, Guid? supplierChainId)
        {
            var groupedFarms = _farmRepository
                                .AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                .ToList();

            return groupedFarms;
        }

        /// <summary>
        /// Overviews the size.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns>PieChart</returns>
        public PieChart OverviewSize(Guid? countryId, Guid? supplierId, Guid? supplierChainId)
        {
            var chart = new PieChart();
            chart.Title = DashboardMessage.Size;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Size;

            var farms = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId) & FarmSpecification.BySize(null, 2));
            item.data.Add(new List<object> { "< 2", farms.Count() });

            farms = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId) & FarmSpecification.BySize(2, 5, true));
            item.data.Add(new List<object> { "2 - 5", farms.Count() });

            farms = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId) & FarmSpecification.BySize(5, 10, true));
            item.data.Add(new List<object> { "5 - 10", farms.Count() });

            farms = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId) & FarmSpecification.BySize(10, null));
            item.data.Add(new List<object> { "> 10", farms.Count() });

            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the variety.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public PieChart OverviewVariety(Guid? countryId, Guid? supplierId, Guid? supplierChainId)
        {
            var chart = new PieChart();
            chart.Title = DashboardMessage.Variety;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Variety;

            var grouped = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                .SelectMany(f => f.Productivity.Plantations)
                .GroupBy(p => p.PlantationVariety.Name);

            foreach (var group in grouped)
            {
                item.data.Add(new List<object> { group.Key, group.Count() });
            }

            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public PieChart OverviewFarms(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var groupedFarms = _farmRepository
                                .AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                .ToList()
                                .GroupBy(f => f.Village, new EntityComparer<Village>());

            var chart = new PieChart();
            chart.Title = DashboardMessage.Farms_Per_Location;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Farms_Per_Location;
            foreach (var group in groupedFarms)
            {
                item.data.Add(new List<object> { group.Key.Name, group.Count() });
            }
            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public PieChart OverviewFarms(Guid countryId, Guid supplierId)
        {
            var groupedFarms = _farmRepository
                                .AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                                .ToList()
                                .GroupBy(f => f.SupplyChain, new EntityComparer<SupplyChain>());

            var chart = new PieChart();
            chart.Title = DashboardMessage.Farms_Per_Location;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Farms_Per_Location;
            foreach (var group in groupedFarms)
            {
                item.data.Add(new List<object> { group.Key.Name, group.Count() });
            }
            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public PieChart OverviewFarms(Guid countryId)
        {
            var groupedFarms = _farmRepository
                                .AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                                .ToList()
                                .GroupBy(f => f.SupplyChain.Supplier, new EntityComparer<Supplier>());

            var chart = new PieChart();
            chart.Title = DashboardMessage.Farms_Per_Location;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Farms_Per_Location;
            foreach (var group in groupedFarms)
            {
                item.data.Add(new List<object> { group.Key.Name, group.Count() });
            }
            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the farms.
        /// </summary>
        /// <returns></returns>
        public PieChart OverviewFarms()
        {
            var groupedFarms = _farmRepository
                                .AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                                .ToList()
                                .GroupBy(f => f.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new PieChart();
            chart.Title = DashboardMessage.Farms_Per_Location;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Farms_Per_Location;
            foreach (var group in groupedFarms)
            {
                item.data.Add(new List<object> { group.Key.Name, group.Count() });
            }
            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public PolarChart OverviewSustainability(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var options = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                            .Select(f => f.ImpactAssessments.OrderByDescending(ia => ia.Date).FirstOrDefault())
                                            .SelectMany(ia => ia.Answers)
                                            .Select(a => a.Id);

            return CreateSustainabilityChart(options);
        }

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public PolarChart OverviewSustainability(Guid countryId, Guid supplierId)
        {
            var options = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                                            .Select(f => f.ImpactAssessments.OrderByDescending(ia => ia.Date).FirstOrDefault())
                                            .SelectMany(ia => ia.Answers)
                                            .Select(a => a.Id);

            return CreateSustainabilityChart(options);
        }

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public PolarChart OverviewSustainability(Guid countryId)
        {

            var options = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                                            .Select(f => f.ImpactAssessments.OrderByDescending(ia => ia.Date).FirstOrDefault())
                                            .SelectMany(ia => ia.Answers)
                                            .Select(a => a.Id);

            return CreateSustainabilityChart(options);
        }

        /// <summary>
        /// Overviews the sustainability.
        /// </summary>
        /// <returns></returns>
        public PolarChart OverviewSustainability()
        {
            var options = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                                            .Select(f => f.ImpactAssessments.OrderByDescending(ia => ia.Date).FirstOrDefault())
                                            .SelectMany(ia => ia.Answers)
                                            .Select(a => a.Id);

            return CreateSustainabilityChart(options);
        }

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public ColumnChart OverviewInvoicesFarms(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                            .SelectMany(f => f.Invoices.Where(i => i.Date.Year == DateTime.UtcNow.Year))
                            .ToList()
                            .GroupBy(i => i.Farm.Village, new EntityComparer<Village>());

            ColumnChart chart = new ColumnChart();
            chart.Categories.Add(DateTime.UtcNow.Year.ToString());
            chart.Title = DashboardMessage.Volume;
            chart.YTitle = DashboardMessage.Volume_Tons;
            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Key.Name;
                item.data.Add(invoice.Sum(i => i.Weight));
                chart.Items.Add(item);
            }

            return chart;
        }

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public ColumnChart OverviewInvoicesFarms(Guid countryId, Guid supplierId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                            .SelectMany(f => f.Invoices.Where(i => i.Date.Year == DateTime.UtcNow.Year))
                            .ToList()
                            .GroupBy(i => i.Farm.SupplyChain, new EntityComparer<SupplyChain>());

            ColumnChart chart = new ColumnChart();
            chart.Categories.Add(DateTime.UtcNow.Year.ToString());
            chart.Title = DashboardMessage.Volume;
            chart.YTitle = DashboardMessage.Volume_Tons;
            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Key.Name;
                item.data.Add(invoice.Sum(i => i.Weight));
                chart.Items.Add(item);
            }

            return chart;
        }

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public ColumnChart OverviewInvoicesFarms(Guid countryId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                            .SelectMany(f => f.Invoices.Where(i => i.Date.Year == DateTime.UtcNow.Year))
                            .ToList()
                            .GroupBy(i => i.Farm.SupplyChain.Supplier, new EntityComparer<Supplier>());

            ColumnChart chart = new ColumnChart();
            chart.Categories.Add(DateTime.UtcNow.Year.ToString());
            chart.Title = DashboardMessage.Volume;
            chart.YTitle = DashboardMessage.Volume_Tons;
            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Key.Name;
                item.data.Add(invoice.Sum(i => i.Weight));
                chart.Items.Add(item);
            }

            return chart;
        }

        /// <summary>
        /// Overviews the invoices farms.
        /// </summary>
        /// <returns></returns>
        public ColumnChart OverviewInvoicesFarms()
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                            .SelectMany(f => f.Invoices.Where(i => i.Date.Year == DateTime.UtcNow.Year))
                            .ToList()
                            .GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            ColumnChart chart = new ColumnChart();
            chart.Categories.Add(DateTime.UtcNow.Year.ToString());
            chart.Title = DashboardMessage.Volume;
            chart.YTitle = DashboardMessage.Volume_Tons;
            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Key.Name;
                item.data.Add(invoice.Sum(i => i.Weight));
                chart.Items.Add(item);
            }

            return chart;
        }

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public PieChart OverviewWorkers(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                .Select(f => f.Worker);

            return CreateWorkersOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the workers coop.
        /// </summary>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <returns></returns>
        public PieChart OverviewWorkersCoop(Guid cooperativeId)
        {
            var workers = db.Farms.Where(f => f.CooperativeId == cooperativeId)
                                .Select(f => f.Worker);

            return CreateWorkersOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public PieChart OverviewWorkers(Guid countryId, Guid supplierId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                                .Select(f => f.Worker);

            return CreateWorkersOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public PieChart OverviewWorkers(Guid countryId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                                .Select(f => f.Worker);

            return CreateWorkersOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the workers.
        /// </summary>
        /// <returns></returns>
        public PieChart OverviewWorkers()
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                                .Select(f => f.Worker);

            return CreateWorkersOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public PieChart OverviewOwnership(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                .Where(f => f.OwnershipTypeId != null)
                                .Select(f => f.OwnershipType);
            return CreateOwnershipOverviewChart(workers);
        }

        public PieChart OverviewOwnershipCoop(Guid cooperativeId)
        {
            var workers = db.Farms.Where(f => f.CooperativeId == cooperativeId).Where(f => f.OwnershipTypeId != null)
                                .Select(f => f.OwnershipType);
            return CreateOwnershipOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public PieChart OverviewOwnership(Guid countryId, Guid supplierId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                                .Where(f => f.OwnershipTypeId != null)
                                .Select(f => f.OwnershipType);
            return CreateOwnershipOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public PieChart OverviewOwnership(Guid countryId)
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                                .Where(f => f.OwnershipTypeId != null)
                                .Select(f => f.OwnershipType);
            return CreateOwnershipOverviewChart(workers);
        }

        /// <summary>
        /// Overviews the ownership.
        /// </summary>
        /// <returns></returns>
        public PieChart OverviewOwnership()
        {
            var workers = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                                .Where(f => f.OwnershipTypeId != null)
                                .Select(f => f.OwnershipType);
            return CreateOwnershipOverviewChart(workers);
        }

        #endregion

        #region evolution
        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <returns></returns>
        public LineChart EvolutionFarms()
        {
            var cache_key = string.Concat(EVOLUTION_FARMS, "GENERAL");
            var chart = CacheFactory.CreateCache().Get<LineChart>(cache_key) as LineChart;
            if (chart == null)
            {
                var assessments = _assessmentRepository
                    .AllMatching(ImpactAssessmentSpecification.FilterDashboard(null, null, null))
                    .AsEnumerable();
                chart = CreateSustainabilityEvolutionChart(assessments);
                CacheFactory.CreateCache().Set<LineChart>(cache_key, chart);
            }

            return chart;
        }

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionFarms(Guid countryId)
        {
            var cache_key = string.Concat(EVOLUTION_FARMS, countryId);
            var chart = CacheFactory.CreateCache().Get<LineChart>(cache_key) as LineChart;
            if (chart == null)
            {
                var assessments = _assessmentRepository
                    .AllMatching(ImpactAssessmentSpecification.FilterDashboard(null, null, countryId))
                    .AsEnumerable();
                chart = CreateSustainabilityEvolutionChart(assessments);
                CacheFactory.CreateCache().Set<LineChart>(cache_key, chart);
            }

            return chart;
        }

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionFarms(Guid countryId, Guid supplierId)
        {
            var cache_key = string.Concat(EVOLUTION_FARMS, supplierId);
            var chart = CacheFactory.CreateCache().Get<LineChart>(cache_key) as LineChart;
            if (chart == null)
            {
                var assessments = _assessmentRepository
                    .AllMatching(ImpactAssessmentSpecification.FilterDashboard(null, supplierId, countryId))
                    .AsEnumerable();
                chart = CreateSustainabilityEvolutionChart(assessments);
                CacheFactory.CreateCache().Set<LineChart>(cache_key, chart);
            }

            return chart;
        }

        /// <summary>
        /// Evolutions the farms.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionFarms(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var cache_key = string.Concat(EVOLUTION_FARMS, supplierChainId);
            var chart = CacheFactory.CreateCache().Get<LineChart>(cache_key) as LineChart;
            if (chart == null)
            {
                var assessments = _assessmentRepository
                    .AllMatching(ImpactAssessmentSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                    .AsEnumerable();
                chart = CreateSustainabilityEvolutionChart(assessments);
                CacheFactory.CreateCache().Set<LineChart>(cache_key, chart);
            }

            return chart;
        }

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <returns></returns>
        public LineChart EvolutionVolume()
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, null))
                                    .SelectMany(f => f.Invoices)
                                    .ToList();

            var grouped = invoices.GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Volume_Evolution;
            chart.YTitle = DashboardMessage.Volume_Tons;
            bool doneCategories = false;
            foreach (var group in grouped)
            {
                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    item.data.Add(year.Sum(i => i.Weight));
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionVolume(Guid countryId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, null, countryId))
                                    .SelectMany(f => f.Invoices)
                                    .ToList();

            var grouped = invoices.GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Volume_Evolution;
            chart.YTitle = DashboardMessage.Volume_Tons;
            bool doneCategories = false;
            foreach (var group in grouped)
            {
                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    item.data.Add(year.Sum(i => i.Weight));
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionVolume(Guid countryId, Guid supplierId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(null, supplierId, countryId))
                                    .SelectMany(f => f.Invoices)
                                    .ToList();

            var grouped = invoices.GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Volume_Evolution;
            chart.YTitle = DashboardMessage.Volume_Tons;
            bool doneCategories = false;
            foreach (var group in grouped)
            {
                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    item.data.Add(year.Sum(i => i.Weight));
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        /// <summary>
        /// Evolutions the volume.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="supplierChainId">The supplier chain identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionVolume(Guid countryId, Guid supplierId, Guid supplierChainId)
        {
            var invoices = _farmRepository.AllMatching(FarmSpecification.FilterDashboard(supplierChainId, supplierId, countryId))
                                    .SelectMany(f => f.Invoices)
                                    .ToList();

            var grouped = invoices.GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Volume_Evolution;
            chart.YTitle = DashboardMessage.Volume_Tons;
            bool doneCategories = false;
            foreach (var group in grouped)
            {
                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    item.data.Add(year.Sum(i => i.Weight));
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        /// <summary>
        /// Evolutions the volume farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionVolumeFarm(Guid farmId)
        {
            var invoices = _farmRepository.Get(farmId).Invoices.ToList();

            var grouped = invoices.GroupBy(i => i.Farm.SupplyChain.Supplier.Country, new EntityComparer<Country>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Volume_Evolution;
            chart.YTitle = DashboardMessage.Volume_Tons;
            bool doneCategories = false;
            foreach (var group in grouped)
            {
                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    item.data.Add(year.Sum(i => i.Weight));
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        #endregion

        #region private methods
        /// <summary>
        /// Creates the ownership overview chart.
        /// </summary>
        /// <param name="ownershipTypes">The ownership types.</param>
        /// <returns></returns>
        private static PieChart CreateOwnershipOverviewChart(IQueryable<OwnershipType> ownershipTypes)
        {
            var grouped = ownershipTypes.ToList()
                .GroupBy(ot => ot.Name);

            var chart = new PieChart();
            chart.Title = DashboardMessage.Ownership_Type;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Ownership_Type;

            foreach (var group in grouped)
            {
                item.data.Add(new List<object> { group.Key, group.Count() });
            }

            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Creates the workers overview chart.
        /// </summary>
        /// <param name="workers">The workers.</param>
        /// <returns></returns>
        private static PieChart CreateWorkersOverviewChart(IQueryable<Worker> workers)
        {
            var chart = new PieChart();
            chart.Title = DashboardMessage.Workers;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Workers;

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Permanent_Workers, Message.Men), 
                workers.Sum(w => w.PermanentMen)});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Permanent_Workers, Message.Women), 
                workers.Sum(w => w.PermanentWomen)});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Temporary_Workers, Message.Men), 
                workers.Sum(w => w.TemporaryMen)});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Temporary_Workers, Message.Women), 
                workers.Sum(w => w.TemporaryWomen)});

            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Creates the sustainability evolution chart.
        /// </summary>
        /// <param name="assessments">The assessments.</param>
        /// <returns></returns>
        private static LineChart CreateSustainabilityEvolutionChart(IEnumerable<ImpactAssessment> assessments)
        {
            int score = 0;
            double count = 0;
            double not_applicable = 0;
            double applicable = 0;
            double fulfill = 0;
            double subtotal = 0;

            var groupedByCategory = assessments
                .SelectMany(ia => ia.Answers)
                .GroupBy(a => a.Criteria.Indicator.Category, new EntityComparer<Category>());

            var chart = new LineChart();
            chart.Title = DashboardMessage.Evolution_Performance;
            chart.YTitle = Message.Score;
            bool doneCategories = false;
            foreach (var group in groupedByCategory)
            {

                var item = new LineSerieItem();
                item.name = group.Key.Name;

                var byYear = group.SelectMany(g => g.ImpactAssessments)
                    .GroupBy(ia => ia.Date.Year)
                    .OrderBy(g => g.Key);

                foreach (var year in byYear)
                {
                    if (!doneCategories)
                    {
                        chart.Categories.Add(year.Key.ToString());
                    }
                    var answers = year
                        .SelectMany(ia => ia.Answers)
                        .Where(co => co.Criteria.Indicator.Category.Id.Equals(group.Key.Id));

                    score = group.Key.Score;
                    count = Convert.ToDouble(answers.Count());
                    not_applicable = Convert.ToDouble(answers.Where(ans => ans.Description.Equals("NA")).Count());
                    applicable = count - not_applicable;
                    fulfill = Convert.ToDouble(answers.Where(ans => ans.Description.Equals("C")).Count());

                    subtotal = fulfill / applicable;
                    item.data.Add(subtotal * score);
                }
                doneCategories = true;
                chart.Items.Add(item);
            }
            return chart;
        }

        /// <summary>
        /// Creates the sustainability chart.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        private PolarChart CreateSustainabilityChart(IEnumerable<Guid> options)
        {
            var categories = _categoryRepository.GetAll();
            var answers = _criteriaOptionRepository.AllMatching(CriteriaOptionSpecification.In(options)).ToList();

            var groupedAnswers = answers.GroupBy(ans => ans.Criteria.Indicator.Category, new EntityComparer<Category>());

            var chart = new PolarChart();
            var ideal = new PolarSerieItem(); ;
            var item = new PolarSerieItem();

            chart.Title = DashboardMessage.Sustainability_Performance;
            item.name = DashboardMessage.Current_Performance;
            ideal.name = DashboardMessage.Ideal_Performance;
            item.pointPlacement = "on";

            foreach (var group in groupedAnswers)
            {
                var category = categories.First(c => c.Id.Equals(group.Key.Id));
                var criteria = category.Indicators.SelectMany(i => i.Criteria);

                var score = category.Score;
                var count = group.Count();
                var not_applicable = Convert.ToDouble(group.Where(ans => ans.Description.Equals("NA") && ans.Criteria.Indicator.CategoryId.Equals(category.Id)).Count());
                var applicable = count - not_applicable;
                var fulfill = Convert.ToDouble(group.Where(ans => ans.Description.Equals("C") && ans.Criteria.Indicator.CategoryId.Equals(category.Id)).Count());

                double subtotal = fulfill / applicable;

                chart.Categories.Add(group.Key.Name);

                item.data.Add(subtotal * score);
                ideal.data.Add(score);
            }
            chart.Items.Add(item);
            chart.Items.Add(ideal);
            return chart;
        }

        #endregion

        #region Farm Dashboard

        /// <summary>
        /// Overviews the workers farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>PieChart</returns>
        public PieChart OverviewWorkersFarm(Guid farmId)
        {
            var worker = _farmRepository.Get(farmId).Worker;
            var chart = new PieChart();
            chart.Title = DashboardMessage.Workers;
            var item = new PieSerieItem();
            item.name = DashboardMessage.Workers;

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Permanent_Workers, Message.Men), 
                worker != null ? worker.PermanentMen : 0});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Permanent_Workers, Message.Women), 
                worker != null ? worker.PermanentWomen : 0});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Temporary_Workers, Message.Men), 
                worker != null ? worker.TemporaryMen : 0});

            item.data.Add(new List<object> { 
                string.Format(FarmMessages.Temporary_Workers, Message.Women), 
                worker != null ? worker.TemporaryWomen : 0});

            chart.Items.Add(item);
            return chart;
        }

        /// <summary>
        /// Overviews the sustainability farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        public PolarChart OverviewSustainabilityFarm(Guid farmId)
        {
            var assessment = _farmRepository.Get(farmId).ImpactAssessments
                                            .OrderByDescending(ia => ia.Date).FirstOrDefault();
            IEnumerable<Guid> options = new List<Guid>();
            if (assessment != null && assessment.Answers != null)
                options = assessment.Answers.Select(a => a.Id);

            return CreateSustainabilityChart(options);
        }

        /// <summary>
        /// Evolutions the farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        public LineChart EvolutionFarm(Guid farmId)
        {
            var assessments = _assessmentRepository
                .AllMatching(ImpactAssessmentSpecification.FilterByFarm(farmId))
                .ToList();

            return CreateSustainabilityEvolutionChart(assessments);
        }

        #endregion
    }
}
