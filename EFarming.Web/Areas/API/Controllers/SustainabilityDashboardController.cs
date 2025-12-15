using EFarming.Core.DashboardModule;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.SustainabilityModule.DashboardAggregate;
using EFarming.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SustainabilityDashboardController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private UnitOfWork db = new UnitOfWork();

        #region General_Reports

        /// <summary>
        /// Clasifications the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PieChart> ClasificationReport()
        {
            var client = new HttpClient();

            //HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["SurveysAPI"] + "TASQ");
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["SurveysAPI"] + "TASQ/=?SurveyId=3");
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            List<TASQClasification> items = JsonConvert.DeserializeObject<List<TASQClasification>>(result);

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Critic Farms";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in items)
            {
                data1.Add(new List<object> { item1.Type, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Class for the list object created
        /// </summary>
        public class GroupedByContacts
        {
            public string attribute { get; set; }
            public int quantity { get; set; }
        }

        /// <summary>
        /// Report for the contacts grouped by type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ContactsReport(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {
            IQueryable<Farm> selectedFarms = db.Farms;
            if (CooperativeId != null)
            {
                CooperativeId = CooperativeId ?? new Guid();
                selectedFarms = db.Farms.Where(c => c.CooperativeId == CooperativeId);
            }
            
            var GroupedContacts = selectedFarms.SelectMany(c => c.Contacts)
                    .Where(d => d.Date >= startDate && d.Date <= endDate)
                    .GroupBy(c => c.TypeId)
                    .Select(gbc => new GroupedByContacts
                    {
                        attribute = gbc.FirstOrDefault().Type.Name,
                        quantity = gbc.Count()
                    }).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Contacts by type";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var CountContact in GroupedContacts)
            {
                data1.Add(new List<object> { CountContact.attribute, CountContact.quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Contacts grouped by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ContactsByUser(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {

            IQueryable<Farm> selectedFarms = db.Farms;
            if (CooperativeId != null)
            {
                CooperativeId = CooperativeId ?? new Guid();
                selectedFarms = db.Farms.Where(c => c.CooperativeId == CooperativeId);
            }
            var GroupedContacts = selectedFarms.SelectMany(c => c.Contacts)
                    .Where(d => d.Date >= startDate && d.Date <= endDate)
                    .GroupBy(c => c.User.FirstName)
                    .Select(gbc => new GroupedByContacts
                    {
                        attribute = gbc.FirstOrDefault().User.FirstName,
                        quantity = gbc.Count()
                    }).ToList();
            GroupedContacts = GroupedContacts.OrderByDescending(c => c.quantity).Take(10).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Top 10 Users";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var CountContact in GroupedContacts)
            {
                data1.Add(new List<object> { CountContact.attribute, CountContact.quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Contacts grouped by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ContactsDrillDown(Guid? CooperativeId, DateTime startDate, DateTime endDate, string Type)
        {

            IQueryable<Farm> selectedFarms = db.Farms;
            if (CooperativeId != null)
            {
                CooperativeId = CooperativeId ?? new Guid();
                selectedFarms = db.Farms.Where(c => c.CooperativeId == CooperativeId);
            }
            var GroupedContacts = selectedFarms.SelectMany(c => c.Contacts)
                    .Where(d => d.Type.Name != "ASSESSMENT")
                    .Where(d => d.Date >= startDate && d.Date <= endDate && d.Type.Name == Type)
                    .GroupBy(c => c.Name)
                    .Select(gbc => new GroupedByContacts
                    {
                        attribute = gbc.FirstOrDefault().Name,
                        quantity = gbc.Count()
                    }).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            var datadrill = new PieSerieItem();
            chart.Title = "Top 10 Users";

            List<List<object>> data1 = new List<List<object>>();
            List<List<object>> data2 = new List<List<object>>();
            foreach (var CountContact in GroupedContacts)
            {
                data1.Add(new List<object> { CountContact.attribute, CountContact.quantity, true });
                data2.Add(new List<object> { CountContact.attribute, CountContact.attribute, CountContact.quantity });
            }

            data.data = data1;
            datadrill.data = data2;

            chart.Items.Add(data);
            //chart.Items.Add(datadrill);

            return chart;
        }

        /// <summary>
        /// Contacts grouped by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<SerieDrillDownItem> ContactsToDrillDown(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {

            IQueryable<Farm> selectedFarms = db.Farms;
            if (CooperativeId != null)
            {
                CooperativeId = CooperativeId ?? new Guid();
                selectedFarms = db.Farms.Where(c => c.CooperativeId == CooperativeId);
            }
            var GroupedContacts = selectedFarms.SelectMany(c => c.Contacts)
                    .Where(d => d.Date >= startDate && d.Date <= endDate)
                    .GroupBy(c => c.TypeId)
                    .Select(gbc => new GroupedByContacts
                    {
                        attribute = gbc.FirstOrDefault().Type.Name,
                        quantity = gbc.Count()
                    }).ToList();

            List<SerieDrillDownItem> serie = new List<SerieDrillDownItem>();
            List<drillObject> elements = new List<drillObject>();
            foreach (var CountContact in GroupedContacts)
            {
                elements.Add(new drillObject {  name = CountContact.attribute, y = CountContact.quantity,  drilldown = CountContact.attribute });
            }
            serie.Add(new SerieDrillDownItem { name = "Contacts", data = elements });

            return serie;
        }
        #endregion
    }
}
