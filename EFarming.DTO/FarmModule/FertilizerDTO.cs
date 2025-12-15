using EFarming.Common;
using System;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// FertilizerDTO HistoricalDTO
    /// </summary>
    public class FertilizerDTO : HistoricalDTO
    {
        public int InvoiceNumber { get; set; }
        public int FarmerIdentification { get; set; }
        public int Ubication { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
        public int Hold { get; set; }
        public int CashRegister { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public FarmDTO Farm { get; set; }
    }
}
