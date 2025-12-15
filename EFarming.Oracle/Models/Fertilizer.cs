using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Oracle.Models
{
    public class Fertilizer
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int InvoiceNumber { get; set; }
        public int FarmerIdentification { get; set; }
        public int Ubication { get; set; }
        public int Value { get; set; }
        public int Hold { get; set; }
        public int CashRegister { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}