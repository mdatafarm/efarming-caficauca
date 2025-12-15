using EFarming.Common.SharedClasses;
using EFarming.Core.DashboardModule;
using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// InvoiceManager Interface
    /// </summary>
    public interface IInvoiceManager
    {
        /// <summary>
        /// By the receipt.
        /// </summary>
        /// <param name="receipt">The receipt.</param>
        /// <returns>InvoiceDTO</returns>
        InvoiceDTO ByReceipt(int receipt);

        /// <summary>
        /// Gets all by farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>ICollection InvoiceDTO</returns>
        ICollection<InvoiceDTO> GetAllByFarm(Guid farmId, DateTime? start, DateTime? end, Guid? lotId);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>InvoiceDTO</returns>
        InvoiceDTO Get(Guid id);

        /// <summary>
        /// Adds the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        void Add(InvoiceDTO invoiceDTO);

        /// <summary>
        /// Edits the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        void Edit(InvoiceDTO invoiceDTO);

        /// <summary>
        /// Removes the specified invoice dto.
        /// </summary>
        /// <param name="invoiceDTO">The invoice dto.</param>
        void Remove(InvoiceDTO invoiceDTO);

        /// <summary>
        /// Tops the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>IEnumerable GroupedInvoice</returns>
        IEnumerable<GroupedInvoice> Top(int count);

        /// <summary>
        /// Sellerses the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>IEnumerable GroupedInvoice</returns>
        IEnumerable<GroupedInvoice> Sellers(int year);

        /// <summary>
        /// Sellerses the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>IEnumerable GroupedInvoice</returns>
        IEnumerable<GroupedInvoice> Sellers(DateTime? start, DateTime? end, Guid? lotId);

        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>IEnumerable InvoiceChartSerie</returns>
        IEnumerable<InvoiceChartSerie> ChartData(int year);

        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>IEnumerable ColumChart</returns>
        IEnumerable<ColumnChart> ChartData(DateTime? start, DateTime? end, Guid? lotId);

        /// <summary>
        /// Charts the data.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>IEnumerable ColumnChart</returns>
        IEnumerable<ColumnChart> ChartData(Guid farmId, DateTime? start, DateTime? end, Guid? lotId);
    }
}
