using AutoMapper;
using EFarming.Common.Resources;
using EFarming.Common.SharedClasses;
using EFarming.Core.DashboardModule;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Repository.TraceabilityModule;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Invoice Manager
    /// </summary>
    public class InvoiceManager : IInvoiceManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IInvoiceRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public InvoiceManager(InvoiceRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all by farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>
        /// ICollection InvoiceDTO
        /// </returns>
        public ICollection<InvoiceDTO> GetAllByFarm(Guid farmId, DateTime? start, DateTime? end, Guid? lotId)
        {
            var data = _repository.AllMatching(InvoiceSpecification.InvoicesByFarm(farmId) & InvoiceSpecification.Filter(start, end, lotId)).OrderBy(i => i.Date);
            return Mapper.Map<ICollection<InvoiceDTO>>(data);
        }

        /// <summary>
        /// Adds the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        public void Add(InvoiceDTO invoiceDTO)
        {
            var invoice = Mapper.Map<Invoice>(invoiceDTO);
            _repository.Add(invoice);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Edits the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        public void Edit(InvoiceDTO invoiceDTO)
        {
            var invoice = Mapper.Map<Invoice>(invoiceDTO);
            var persisted = _repository.Get(invoice.Id);
            _repository.Merge(persisted, invoice);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Removes the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        public void Remove(InvoiceDTO invoiceDTO)
        {
            var invoice = _repository.Get(invoiceDTO.Id);
            _repository.Remove(invoice);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// InvoiceDTO
        /// </returns>
        public InvoiceDTO Get(Guid id)
        {
            return Mapper.Map<InvoiceDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Tops the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// IEnumerable GroupedInvoice
        /// </returns>
        public IEnumerable<GroupedInvoice> Top(int count)
        {
            return _repository.GetAll()
                .GroupBy(i => new { i.FarmId, i.Farm.Name })
                .Select(grp => new GroupedInvoice
                {
                    Farm = grp.Key.Name,
                    Weight = grp.Sum(g => g.Weight),
                    Price = grp.Sum(g => g.Weight * g.Value),
                })
                .OrderByDescending(s => s.Weight).ThenByDescending(s => s.Price)
                .Take(count);
        }

        /// <summary>
        /// Sellerses the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// IEnumerable GroupedInvoice
        /// </returns>
        public IEnumerable<GroupedInvoice> Sellers(int year)
        {
            return _repository.AllMatching(InvoiceSpecification.ByYear(year))
                .GroupBy(i => new { i.FarmId, i.Farm.Name })
                .Select(grp => new GroupedInvoice
                {
                    Farm = grp.Key.Name,
                    Weight = grp.Sum(g => g.Weight),
                    Price = grp.Sum(g => g.Weight * g.Value),
                })
                .OrderByDescending(s => s.Weight).ThenByDescending(s => s.Price);
        }

        /// <summary>
        /// Sellerses the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>
        /// IEnumerable GroupedInvoice
        /// </returns>
        public IEnumerable<GroupedInvoice> Sellers(DateTime? start, DateTime? end, Guid? lotId)
        {
            return _repository.AllMatching(InvoiceSpecification.Filter(start, end, lotId))
                .GroupBy(i => new { i.FarmId, i.Farm.Name })
                .Select(grp => new GroupedInvoice
                {
                    Farm = grp.Key.Name,
                    Weight = grp.Sum(g => g.Weight),
                    Price = grp.Sum(g => g.Weight * g.Value),
                })
                .OrderByDescending(s => s.Weight).ThenByDescending(s => s.Price);
        }

        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// IEnumerable InvoiceChartSerie
        /// </returns>
        public IEnumerable<InvoiceChartSerie> ChartData(int year)
        {
            var serieWeight = new InvoiceChartSerie { name = TraceabilityMessage.Weight };
            var seriePrice = new InvoiceChartSerie { name = TraceabilityMessage.Price };

            var groupedInvoices = _repository.AllMatching(InvoiceSpecification.ByYear(year))
                .GroupBy(i => i.Date.Month);

            foreach (var group in groupedInvoices)
            {
                serieWeight.data.Add(new List<object> { CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key), group.Sum(i => i.Weight) });
                seriePrice.data.Add(new List<object> { CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key), group.Sum(i => i.Value) });
            }

            return new List<InvoiceChartSerie> { serieWeight, seriePrice };
        }


        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>
        /// IEnumerable ColumChart
        /// </returns>
        public IEnumerable<ColumnChart> ChartData(DateTime? start, DateTime? end, Guid? lotId)
        {
            var invoices = _repository.AllMatching(InvoiceSpecification.Filter(start, end, lotId));
            return CreateChart(invoices);
        }

        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>
        /// IEnumerable ColumnChart
        /// </returns>
        public IEnumerable<ColumnChart> ChartData(Guid farmId, DateTime? start, DateTime? end, Guid? lotId)
        {
            var invoices = _repository.AllMatching(InvoiceSpecification.InvoicesByFarm(farmId) & InvoiceSpecification.Filter(start, end, lotId));
            return CreateChart(invoices);
        }

        /// <summary>
        /// Creates the chart.
        /// </summary>
        /// <param name="invoices">The invoices.</param>
        /// <returns></returns>
        private static IEnumerable<ColumnChart> CreateChart(IQueryable<Invoice> invoices)
        {
            var groupedInvoices = invoices.GroupBy(i => i.Date.Month).OrderBy(g => g.Key);
            var years = invoices.GroupBy(i => i.Date.Year).OrderBy(g => g.Key).Select(g => g.Key);

            var serieWeight = new ColumnChart { Title = TraceabilityMessage.Weight, YTitle = TraceabilityMessage.Weight };
            var seriePrice = new ColumnChart { Title = TraceabilityMessage.Price, YTitle = TraceabilityMessage.Price };
            serieWeight.Categories = seriePrice.Categories = years.Select(y => y.ToString()).ToList();
            foreach (var item in groupedInvoices)
            {
                var weightItem = new ColumnSerieItem();
                var priceItem = new ColumnSerieItem();
                weightItem.name = priceItem.name =
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Key);
                foreach (var year in years)
                {
                    var foryears = item.Where(i => i.Date.Year == year);
                    weightItem.data.Add(foryears.Sum(i => i.Weight));
                    priceItem.data.Add(foryears.Sum(i => i.Value));
                }
                serieWeight.Items.Add(weightItem);
                seriePrice.Items.Add(priceItem);
            }

            return new List<ColumnChart> { serieWeight, seriePrice };
        }

        /// <summary>
        /// By the receipt.
        /// </summary>
        /// <param name="receipt">The receipt.</param>
        /// <returns>
        /// InvoiceDTO
        /// </returns>
        public InvoiceDTO ByReceipt(int receipt)
        {
            return Mapper.Map<InvoiceDTO>(_repository.AllMatching(InvoiceSpecification.InvoicesByReceipt(receipt)).FirstOrDefault());
        }
    }
}
