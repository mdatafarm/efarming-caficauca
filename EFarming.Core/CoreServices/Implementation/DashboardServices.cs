using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.Common.Resources;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.CoreServices.Contracts;
using EFarming.Core.DashboardModule;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.CoreServices.Implementation
{
    /// <summary>
    /// DashboarService Implementation
    /// </summary>
    public class DashboardServices : IDashboardServices
    {
        /// <summary>
        /// The _department repository
        /// </summary>
        private IDepartmentRepository _departmentRepository;
        /// <summary>
        /// The _municipality repository
        /// </summary>
        private IMunicipalityRepository _municipalityRepository;
        /// <summary>
        /// The _indicator repository
        /// </summary>
        private IIndicatorRepository _indicatorRepository;
        /// <summary>
        /// The _impact assessment repository
        /// </summary>
        private IImpactAssessmentRepository _impactAssessmentRepository;
        /// <summary>
        /// The _sensory profile repository
        /// </summary>
        private ISensoryProfileRepository _sensoryProfileRepository;
        /// <summary>
        /// The _quality attribute repository
        /// </summary>
        private IQualityAttributeRepository _qualityAttributeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardServices"/> class.
        /// </summary>
        /// <param name="departmentRepository">The department repository.</param>
        /// <param name="municipalityRepository">The municipality repository.</param>
        /// <param name="indicatorRepository">The indicator repository.</param>
        /// <param name="impactAssessmentRepository">The impact assessment repository.</param>
        /// <param name="sensoryProfileRepository">The sensory profile repository.</param>
        /// <param name="qualityAttributeRepository">The quality attribute repository.</param>
        public DashboardServices(
            IDepartmentRepository departmentRepository,
            IMunicipalityRepository municipalityRepository,
            IIndicatorRepository indicatorRepository,
            IImpactAssessmentRepository impactAssessmentRepository,
            ISensoryProfileRepository sensoryProfileRepository,
            IQualityAttributeRepository qualityAttributeRepository)
        {
            _departmentRepository = departmentRepository;
            _municipalityRepository = municipalityRepository;
            _indicatorRepository = indicatorRepository;
            _impactAssessmentRepository = impactAssessmentRepository;
            _sensoryProfileRepository = sensoryProfileRepository;
            _qualityAttributeRepository = qualityAttributeRepository;
        }

        /// <summary>
        /// Farmses the by location.
        /// </summary>
        /// <returns>data</returns>
        public Dictionary<string, int> FarmsByLocation()
        {
            var departments = _departmentRepository.GetAll("Municipalities", "Municipalities.Villages").ToList();
            Dictionary<string, int> data = departments.ToDictionary(d => d.Name, d => d.CountFarms());
            return data;
        }


        /// <summary>
        /// Plantationses the by location.
        /// </summary>
        /// <returns>data</returns>
        public Dictionary<string, int> PlantationsByLocation()
        {
            var departments = _departmentRepository.GetAll(
                "Municipalities",
                "Municipalities.Villages",
                "Municipalities.Villages.Farms",
                "Municipalities.Villages.Farms.Productivity",
                "Municipalities.Villages.Farms.Productivity.Plantations",
                "Municipalities.Villages.Farms.Productivity.Plantations.PlantationType");

            IEnumerable<PlantationType> plantationTypes = new List<PlantationType>();
            if (departments != null)
            {
                foreach (var department in departments)
                {
                    plantationTypes = plantationTypes.Concat(department.GetPlantationTypes());
                }
            }
            return plantationTypes.GroupBy(pt => pt.Name).ToDictionary(pt => pt.Key, pt => pt.Count());
        }


        /// <summary>
        /// Impacts the by location.
        /// </summary>
        /// <returns>data</returns>
        public object ImpactByLocation()
        {
            var departments = _departmentRepository.GetAll(
                "Municipalities",
                "Municipalities.Villages",
                "Municipalities.Villages.Farms",
                "Municipalities.Villages.Farms.ImpactAssessments",
                "Municipalities.Villages.Farms.ImpactAssessments.Answers.Criteria",
                "Municipalities.Villages.Farms.ImpactAssessments.Answers.Criteria.Indicator");

            var indicators = _indicatorRepository.GetAll().ToList();
            List<Dictionary<string, object>> series = new List<Dictionary<string, object>>();
            foreach (var department in departments)
            {
                var answers = department.GetImpactAnswers();
                var dict = new Dictionary<string, object>();
                List<int> values = new List<int>();
                foreach (var ans in answers)
                {
                    bool exists = indicators.Contains(ans.Key);
                    if (exists)
                    {
                        values.Add(ans.Value);
                    }
                    else
                    {
                        values.Add(0);
                    }
                }
                dict.Add("name", department.Name);
                dict.Add("data", values);
                series.Add(dict);
            }

            var categories = indicators.Select(i => i.Name);

            var a = new { categories = categories, series = series, title = DashboardMessage.Impact_Performance };
            return a;
        }


        /// <summary>
        /// Tracks the impact by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>data</returns>
        public object TrackImpactByLocation(int year)
        {
            var result = _impactAssessmentRepository.AllMatching(ImpactAssessmentSpecification.TrackByLocation(year), "Answers.Criteria").ToList();
            return CalculateImpactTrack(year, result);
        }


        /// <summary>
        /// Qualities the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>data</returns>
        public BarChart QualityByLocation(int year)
        {
            var sql = _sensoryProfileRepository.AllMatching(
                SensoryProfileAssessmentSpecification.TrackByLocation(year), 
                "SensoryProfileAnswers.QualityAttribute",
                "Farm.Village.Municipality")
                .SelectMany(sp => sp.SensoryProfileAnswers)
                .Where(spa => spa.QualityAttribute.TypeOf.Equals(QualityAttributeTypes.RANGE))
                .GroupBy(spa => spa.SensoryProfileAssessment.Farm.Village.Municipality.Department).ToList();
            var result = sql
                .Select(group => new
                {
                    Department = group.Key,
                    Answers = group.ToList()
                });

            var qualityAttributes = _qualityAttributeRepository
                .AllMatching(QualityAttributeSpecification.CuantitativeAttributes())
                .ToList();

            BarChart chart = new BarChart
            {
                Title = DashboardMessage.Quality_Performance,
                Categories = qualityAttributes.Select(qa => qa.Description).ToList()
            };

            foreach (var item in result)
            {
                var groupedAnswers = item.Answers
                    .GroupBy(ans => ans.QualityAttribute)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Average(a => Convert.ToDouble(a.Answer)));

                var serieItem = new SerieItem { name = item.Department.Name };
                foreach (var attribute in qualityAttributes)
                {
                    //if (groupedAnswers.Keys.Contains(attribute))
                    //{
                    //    serieItem.data.Add(groupedAnswers[attribute]);
                    //}
                    //else
                    //{
                    //    serieItem.data.Add(0);
                    //}
                }
                chart.Items.Add(serieItem);
            }

            return chart;
        }


        /// <summary>
        /// Farmses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>data</returns>
        public Dictionary<string, int> FarmsByDepartment(Guid id)
        {
            var municipalities = _municipalityRepository
                                .GetAll("Villages")
                                .Where(d => d.DepartmentId.Equals(id))
                                .ToList();
            Dictionary<string, int> data = municipalities.ToDictionary(d => d.Name, d => d.CountFarms());
            return data;
        }


        /// <summary>
        /// Plantationses the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>data</returns>
        public Dictionary<string, int> PlantationsByDepartment(Guid id)
        {
            var municipalities = _municipalityRepository.GetAll(
                "Villages",
                "Villages.Farms",
                "Villages.Farms.Productivity",
                "Villages.Farms.Productivity.Plantations",
                "Villages.Farms.Productivity.Plantations.PlantationType")
                .Where(m => m.DepartmentId.Equals(id));

            IEnumerable<PlantationType> plantationTypes = new List<PlantationType>();
            if (municipalities != null)
            {
                foreach (var municipality in municipalities)
                {
                    plantationTypes = plantationTypes.Concat(municipality.GetPlantationTypes());
                }
            }
            return plantationTypes.GroupBy(pt => pt.Name).ToDictionary(pt => pt.Key, pt => pt.Count());
        }


        /// <summary>
        /// Impacts the by department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>data</returns>
        public object ImpactByDepartment(Guid id)
        {
            var departments = _municipalityRepository.GetAll(
                "Villages",
                "Villages.Farms",
                "Villages.Farms.ImpactAssessments",
                "Villages.Farms.ImpactAssessments.Answers.Criteria",
                "Villages.Farms.ImpactAssessments.Answers.Criteria.Indicator")
                .Where(m => m.DepartmentId.Equals(id));

            var indicators = _indicatorRepository.GetAll().ToList();
            List<Dictionary<string, object>> series = new List<Dictionary<string, object>>();
            foreach (var department in departments)
            {
                var answers = department.GetImpactAnswers();
                var dict = new Dictionary<string, object>();
                List<int> values = new List<int>();
                foreach (var ans in answers)
                {
                    bool exists = indicators.Contains(ans.Key);
                    if (exists)
                    {
                        values.Add(ans.Value);
                    }
                    else
                    {
                        values.Add(0);
                    }
                }
                dict.Add("name", department.Name);
                dict.Add("data", values);
                series.Add(dict);
            }

            var categories = indicators.Select(i => i.Name);

            var a = new { categories = categories, series = series, title = DashboardMessage.Impact_Performance };
            return a;
        }


        /// <summary>
        /// Tracks the impact by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>data</returns>
        public object TrackImpactByDepartment(int year, Guid id)
        {
            var result = _impactAssessmentRepository.AllMatching(
                ImpactAssessmentSpecification.TrackByDepartment(year, id), "Answers.Criteria", "Farm.Village.Municipality").ToList();
            return CalculateImpactTrack(year, result);
        }

        /// <summary>
        /// Calculates the impact track.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="result">The result.</param>
        /// <returns>data</returns>
        private object CalculateImpactTrack(int year, List<ImpactAssessment> result)
        {
            var assessments = result
                .GroupBy(g => g.Date.Month)
                .Select(ag => new
                {
                    Month = ag.Key,
                    Assessments = ag.SelectMany(a => a.Answers)
                })
                .ToDictionary(kvp => kvp.Month, kvp => kvp.Assessments);

            List<Dictionary<string, object>> series = new List<Dictionary<string, object>>();
            var indicators = _indicatorRepository.GetAll().ToList();
            foreach (var indicator in indicators)
            {
                var dict = new Dictionary<string, object>();
                List<double> values = new List<double>();
                for (int month = 1; month <= 12; month++)
                {
                    if (assessments.ContainsKey(month))
                    {
                        values.Add(assessments[month].Where(an => an.Criteria.IndicatorId.Equals(indicator.Id)).Average(an => an.Value));
                    }
                }

                dict.Add("name", indicator.Name);
                dict.Add("data", values);
                series.Add(dict);
            }

            return new
            {
                categories = assessments.Keys.Select(k => new DateTime(1, k, 1).ToString("MMM")),
                series = series,
                title = string.Format(DashboardMessage.Impact_Track, year)
            };
        }


        /// <summary>
        /// Qualities the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>data</returns>
        public BarChart QualityByDepartment(int year, Guid id)
        {
            var sql = _sensoryProfileRepository.AllMatching(
                SensoryProfileAssessmentSpecification.TrackByDepartment(year, id),
                "SensoryProfileAnswers.QualityAttribute",
                "Farm.Village.Municipality")
                .SelectMany(sp => sp.SensoryProfileAnswers)
                .Where(spa => spa.QualityAttribute.TypeOf.Equals(QualityAttributeTypes.RANGE))
                .GroupBy(spa => spa.SensoryProfileAssessment.Farm.Village.Municipality).ToList();
            var result = sql
                .Select(group => new
                {
                    Department = group.Key,
                    Answers = group.ToList()
                });

            var qualityAttributes = _qualityAttributeRepository
                .AllMatching(QualityAttributeSpecification.CuantitativeAttributes())
                .ToList();

            BarChart chart = new BarChart
            {
                Title = DashboardMessage.Quality_Performance,
                Categories = qualityAttributes.Select(qa => qa.Description).ToList()
            };

            foreach (var item in result)
            {
                var groupedAnswers = item.Answers
                    .GroupBy(ans => ans.QualityAttribute)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Average(a => Convert.ToDouble(a.Answer)));

                var serieItem = new SerieItem { name = item.Department.Name };
                foreach (var attribute in qualityAttributes)
                {
                    //if (groupedAnswers.Keys.Contains(attribute))
                    //{
                    //    serieItem.data.Add(groupedAnswers[attribute]);
                    //}
                    //else
                    //{
                    //    serieItem.data.Add(0);
                    //}
                }
                chart.Items.Add(serieItem);
            }

            return chart;
        }
    }
}
