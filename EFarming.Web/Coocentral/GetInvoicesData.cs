using EFarming.DAL;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Oracle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EFarming.Web.Coocentral
{
    public class GetInvoicesData
    {
        private IInvoiceManager _manager;
        public GetInvoicesData(
            InvoiceManager manager
            )
        {
            _manager = manager;
        }
        private UnitOfWork db = new UnitOfWork();
        public async Task<string> GetInvoicesInformation(IFarmManager _farmmanager, DateTime LastInvoice)
        {
            var EndDate = DateTime.Now.AddDays(-1);
            string answer = null;
            // string DateClasue = "and Fac.facf_fecha between TO_DATE('" + LastInvoice.Year + "/" + LastInvoice.Month + "/" + LastInvoice.Day + "', 'yyyy/mm/dd') and TO_DATE('" + EndDate.Year + "/" + EndDate.Month + "/" + EndDate.Day + "', 'yyyy/mm/dd')";
            string DateClasue = "and Operation.\"Date\" between'" + LastInvoice.Day + "/" + LastInvoice.Month + "/" + LastInvoice.Year + "' and '" + EndDate.Day + "/" + EndDate.Month + "/" + EndDate.Year + "'";
            //Conection with the Oracle API.
            //Saving the JSON in an Invoice object list
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["OracleAPI"] + "invoices?DateClause=" + DateClasue);

            try
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                if (result != "\"doesn't exist data\"")
                {
                    List<Invoice> items = JsonConvert.DeserializeObject<List<Invoice>>(result);

                    //Loop for each family member in the list
                    foreach (var invoice in items)
                    {
                        InvoiceDTO invoiceToAdd = new InvoiceDTO();
                        invoiceToAdd = CreateinvoiceDTO(invoice, invoiceToAdd);

                        //Farm verified, if the farm exists, the information is updated
                        var FarmExists = _farmmanager.GetFarmByCode(invoice.FarmerIdentification.ToString());
                        if (FarmExists.Code != null)
                        {
                            invoiceToAdd.FarmId = FarmExists.Id;
                            //Reading the family memebers and deleting the actual people
                            foreach (var FarmInvoice in FarmExists.Invoices.ToList())
                            {
                                if (FarmInvoice.InvoiceNumber == invoiceToAdd.InvoiceNumber)
                                {
                                    _manager.Remove(FarmInvoice);
                                }
                            }
                            //Adding the invoice to the farm
                            if(invoiceToAdd.CoffeeTypeId != 0)
                            {
                                _manager.Add(invoiceToAdd);
                            }
                        }
                    }
                    answer = "Success";
                }
                else
                    answer = result;
            }
            catch (Exception e)
            {
                answer = e.Message;
            }
            return answer;
        }

        private InvoiceDTO CreateinvoiceDTO(Invoice invoice, InvoiceDTO invoiceToAdd)
        {
            //The date is verified, if this date has a wrong format is replaced by the actual date
            try
            {
                invoiceToAdd.InvoiceNumber = invoice.InvoiceNumber;
                invoiceToAdd.Identification = invoice.FarmerIdentification.ToString();
                invoiceToAdd.Value = invoice.Value;
                invoiceToAdd.Ubication = invoice.Ubication;
                invoiceToAdd.Hold = invoice.Hold;
                invoiceToAdd.Cash = invoice.Cash;
                invoiceToAdd.Weight = invoice.Weight;
                invoiceToAdd.BaseKg = invoice.BaseKg;
                //var productId = Int32.Parse(invoice.CoffeeTypeId.ToString().Substring(invoice.CoffeeTypeId.ToString().Length - 2));
                var productId = Int32.Parse(invoice.CoffeeTypeId.ToString());
                var coffetypeId = db.CoffeeType.Where(t => t.Identifier == productId).FirstOrDefault();
                invoiceToAdd.CoffeeTypeId = coffetypeId.Id;
                invoiceToAdd.DateInvoice = Convert.ToDateTime(invoice.Date);
                invoiceToAdd.Date = Convert.ToDateTime(invoice.Date);
            }
            catch (Exception DateException)
            {
                var DateError = DateException;
                invoiceToAdd.DateInvoice = DateTime.Now;
                invoiceToAdd.Date = DateTime.Now;
            }

            return invoiceToAdd;
        }

    }
}