using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Oracle.Models
{
    public class Invoice
    {
        public int FarmerIdentification { get; set; }
        public int Weight { get; set; }
        public string Date { get; set; }
        public int InvoiceNumber { get; set; }
        public int Value { get; set; }
        public string DateInvoice { get; set; }
        public int Ubication { get; set; }
        public int Hold { get; set; }
        public int Cash { get; set; }
        public int BaseKg { get; set; }
        public int CoffeeTypeId { get; set; }
    }
}