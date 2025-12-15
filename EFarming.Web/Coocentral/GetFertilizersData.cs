using EFarming.DTO.FarmModule;
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
    public class GetFertilizersData
    {
        public async Task<string> GetFertilizersInformation(IFarmManager _farmmanager, DateTime LastFertilizer)
        {
            var EndDate = DateTime.Now.AddDays(-1);
            string answer = null;
            //Last fertilizer date
            // string DateClasue = "and Fac.facf_fecha between TO_DATE('" + LastFertilizer.Year + "/" + LastFertilizer.Month + "/" + LastFertilizer.Day + "', 'yyyy/mm/dd') and TO_DATE('" + EndDate.Year + "/" + EndDate.Month + "/" + EndDate.Day + "', 'yyyy/mm/dd')";
            string DateClasue = "and Operation.\"Date\" between'" + LastFertilizer.Day + "/" + LastFertilizer.Month + "/" + LastFertilizer.Year + "' and '" + EndDate.Day + "/" + EndDate.Month + "/" + EndDate.Year + "'";
            //Conection with the Oracle API.
            //Saving the JSON in an Fertilizer object list
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["OracleAPI"] + "fertilizers?DateClause=" + DateClasue);

            try
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                if(result != "\"doesn't exist data\"")
                {
                    List<Fertilizer> items = JsonConvert.DeserializeObject<List<Fertilizer>>(result);

                    //Loop for each family member in the list
                    foreach (var fertilizer in items)
                    {
                        FertilizerDTO fertilizerToAdd = new FertilizerDTO();
                        fertilizerToAdd = CreateFertilizerDTO(fertilizer, fertilizerToAdd);

                        //Farm verified, if the farm exists, the information is updated
                        var FarmExists = _farmmanager.GetFarmByCode(fertilizer.FarmerIdentification.ToString());
                        if (FarmExists.Code != null)
                        {
                            fertilizerToAdd.FarmId = FarmExists.Id;
                            //Reading the fertilizers and deleting the actual
                            //foreach (var farmFertilizer in FarmExists.Fertilizers.ToList())
                            //{
                            //    if (farmFertilizer.InvoiceNumber == fertilizerToAdd.InvoiceNumber)
                            //    {
                            //        FarmExists.Fertilizers.Remove(farmFertilizer);
                            //    }
                            //}
                            FarmExists.Fertilizers.Add(fertilizerToAdd);
                            //Adding the people to the farm
                            _farmmanager.Edit(FarmExists.Id, FarmExists, FarmManager.FERTILIZERS);
                        }
                    }
                    answer = "Success";
                }else
                    answer = result;
            }
            catch(Exception e)
            {
                answer = e.Message;
            }

            return answer;
        }

        private FertilizerDTO CreateFertilizerDTO(Fertilizer fertilizer, FertilizerDTO fertilizerToAdd)
        {
            //The date is verified, if this date has a wrong format is replaced by the actual date
            try
            {
                fertilizerToAdd.InvoiceNumber = fertilizer.InvoiceNumber;
                fertilizerToAdd.FarmerIdentification = fertilizer.FarmerIdentification;
                fertilizerToAdd.Ubication = fertilizer.Ubication;
                fertilizerToAdd.Value = fertilizer.Value;
                fertilizerToAdd.Hold = fertilizer.Hold;
                fertilizerToAdd.CashRegister = fertilizer.CashRegister;
                fertilizerToAdd.UnitPrice = fertilizer.UnitPrice;
                fertilizerToAdd.Quantity = fertilizer.Quantity;
                fertilizerToAdd.Name = fertilizer.Name;
                fertilizerToAdd.Date = Convert.ToDateTime(fertilizer.Date);
            }
            catch (Exception DateException)
            {
                var DateError = DateException;
                fertilizerToAdd.Date = DateTime.Now;
            }

            return fertilizerToAdd;
        }
    }
}