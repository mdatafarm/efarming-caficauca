using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Repository.AdminModule;
using EFarming.Repository.FarmModule;
using EFarming.Repository.ProjectModule;
using EFarming.Web.Controllers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Web.Util
{
    public class UpdateProductivityJob : IJob
    {
        private UnitOfWork db = new UnitOfWork();
        private IFarmManager _manager;


        public void Execute(IJobExecutionContext context)
        {
            FarmRepository farmRepository = new FarmRepository(db);
            SoilTypeRepository soilTypeRepository = new SoilTypeRepository(db);
            FamilyUnitRepository familyUnitRepository = new FamilyUnitRepository(db);
            UserRepository userRepository = new UserRepository(db);
            ProjectRepository projectRepository = new ProjectRepository(db);
            Storage storage = new Storage();

            FarmManager farmMananger = new FarmManager(farmRepository, soilTypeRepository, familyUnitRepository, userRepository, projectRepository, storage);
            IFarmManager _manager = farmMananger;
            

            var Farms = db.Farms.ToList();

            //FarmDTO farm = new FarmDTO();

            /*
            foreach (var itemf in Farms)
            {
                var farm = _manager.Details(itemf.Id);

                List<PlantationDTO> listplantation = farm.Productivity.Plantations.ToList();

                foreach (var item in listplantation)
                {
                    item.UpdatedAt = DateTime.Now;
                    var farmdto = _manager.Details(itemf.Id);
                    var toRemove = farmdto.Productivity.Plantations.First(p => p.Id.Equals(item.Id));
                    farmdto.Productivity.Plantations.Remove(toRemove);
                    farmdto.Productivity.Plantations.Add(item);
                    _manager.Edit(farmdto.Id, farmdto, FarmManager.PLANTATIONS);
                    farmdto = _manager.Details(item.FarmId);
                }
            }
            */

            var fincas = db.Farms.ToList();


            foreach (var finca in fincas)
            {

                var productivitiesChange2 = db.Productivities.FirstOrDefault(f => f.Id == finca.Id);

                decimal totalHectareas = 0;
                decimal totalHectareas_SumPerc = 0;
                decimal totalHectareasCoffe = 0;


                if (productivitiesChange2 != null)
                {


                    if (productivitiesChange2.Plantations.Count > 0)
                    {
                        foreach (var p in productivitiesChange2.Plantations)
                        {
                            if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                            {
                                totalHectareasCoffe = totalHectareasCoffe + Convert.ToDecimal(p.Hectares);
                            }

                            totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                        }
                    }

                    double averageDensity = 0;
                    double averageAge = 0;
                    string now = DateTime.Now.ToString();

                    var plants2 = productivitiesChange2.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));

                    foreach (var plantation in plants2)
                    {
                        plantation.Hectares = plantation.Hectares.Replace(".", ",");
                        plantation.EstimatedProduction = plantation.EstimatedProduction.Replace(".", ",");
                        double percentage = (Convert.ToDouble(plantation.Hectares) * 1.0) / Convert.ToDouble(totalHectareasCoffe);
                        TimeSpan dateAge = DateTime.Now.Subtract(plantation.Age);
                        double Age = (dateAge.Days * 1.0) / 365;

                        averageAge += Age * percentage;

                        averageDensity += (Convert.ToDouble(plantation.Density.Replace(".", ",")) * 1.0) * percentage;
                    }

                    if (averageAge.ToString() == "NaN")
                    {
                        averageAge = 0;
                    }
                    productivitiesChange2.coffeeArea = totalHectareasCoffe.ToString().Replace(".", ",");
                    productivitiesChange2.averageDensity = averageDensity.ToString();
                    productivitiesChange2.averageAge = Math.Round(averageAge, 3);
                    db.SaveChanges();

                    var farm = _manager.Details(finca.Id,
                        "Productivity",
                        "Contacts",
                        "Productivity.Plantations",
                        "Productivity.Plantations.FloweringPeriods",
                        "Worker",
                        "OtherActivities",
                        "SoilAnalysis",
                        "SoilTypes",
                        "Fertilizers",
                        "FamilyUnitMembers",
                        "ImpactAssessments",
                        "ImpactAssessments.Answers",
                        "Village",
                        "Village.Municipality",
                        "Village.Municipality.Department");



                    decimal infHct = farm.Productivity.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.InfrastructureHectares);
                    decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
                    decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
                    //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

                    totalHectareas_SumPerc = totalHectareas;
                    totalHectareas = totalHectareas + infHct + fpHct + conHct;

                    if (farm.Productivity.TotalHectares != totalHectareas.ToString())
                    {
                        try
                        {
                            farm.Productivity.TotalHectares = totalHectareas.ToString();

                            var productivity = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                            productivity.TotalHectares = totalHectareas.ToString();
                            db.SaveChanges();
                        }
                        catch
                        {
                            farm.Productivity.TotalHectares = totalHectareas.ToString();
                        }
                    }

                    var idFarm = farm.Id;

                    //PORCENTAJE COLOMBIA
                    var PerColombia = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
                    decimal sumPerColombia = 0;
                    foreach (var item in PerColombia)
                    {
                        sumPerColombia = sumPerColombia + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE CATURRA
                    var PerCaturra = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
                    decimal sumPerCaturra = 0;
                    foreach (var item in PerCaturra)
                    {
                        sumPerCaturra = sumPerCaturra + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE CASTILLO
                    var PerCastillo = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
                    decimal sumPerCastillo = 0;
                    foreach (var item in PerCastillo)
                    {
                        sumPerCastillo = sumPerCastillo + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE OTRO
                    var PerOtro = db.Plantations.Where(x => x.ProductivityId == idFarm);
                    decimal sumPerOtro = 0;
                    foreach (var item in PerOtro)
                    {
                        if (item.PlantationVarietyId != new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7") && item.PlantationVarietyId != new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C") && item.PlantationVarietyId != new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"))
                        {
                            sumPerOtro = sumPerOtro + Convert.ToDecimal(item.Hectares);
                        }
                    }

                    //OPERACIONES PORCENTAJES
                    var opeColombia = Convert.ToDecimal(0);
                    if (sumPerColombia != 0 && totalHectareasCoffe != 0)
                    {
                        opeColombia = (sumPerColombia / (totalHectareas_SumPerc)) * 100;
                        if (opeColombia > 100)
                        {
                            opeColombia = 100;
                        }
                    }

                    var opeCaturra = Convert.ToDecimal(0);
                    if (sumPerCaturra != 0 && totalHectareasCoffe != 0)
                    {
                        opeCaturra = (sumPerCaturra / (totalHectareas_SumPerc)) * 100;
                        if (opeCaturra > 100)
                        {
                            opeCaturra = 100;
                        }
                    }

                    var opeCastillo = Convert.ToDecimal(0);
                    if (sumPerCastillo != 0 && totalHectareasCoffe != 0)
                    {
                        opeCastillo = (sumPerCastillo / (totalHectareas_SumPerc)) * 100;
                        if (opeCastillo > 100)
                        {
                            opeCastillo = 100;
                        }
                    }

                    var opeOtro = Convert.ToDecimal(0);
                    if (sumPerOtro != 0 && totalHectareasCoffe != 0)
                    {
                        opeOtro = (sumPerOtro / (totalHectareas_SumPerc)) * 100;
                        if (opeOtro > 100)
                        {
                            opeOtro = 100;
                        }
                    }
                    else if (sumPerOtro != 0 && totalHectareasCoffe == 0)
                    {
                        opeOtro = (sumPerOtro / sumPerOtro) * 100;
                        if (opeOtro > 100)
                        {
                            opeOtro = 100;
                        }
                    }


                    //ACTUALIZACION
                    var sum = opeColombia + opeCaturra + opeCastillo + opeOtro;
                    var dif = 0.0;
                    if (sum > 100)
                    {
                        dif = Convert.ToDouble(sum) - Convert.ToDouble(100);
                        if (Math.Round(Convert.ToDecimal(opeOtro), 2) != Math.Round(Convert.ToDecimal(dif), 2))
                        {
                            opeOtro = Convert.ToDecimal(opeOtro) - Convert.ToDecimal(dif);
                        }
                    }
                    else if (opeColombia == 0 && opeCaturra == 0 && opeCastillo == 0 && opeOtro != 0)
                    {
                        opeOtro = 100;
                    }


                    var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                    productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
                    productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
                    productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
                    productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
                    productivitiesChange.UpdatedAt = DateTime.Now;
                    db.SaveChanges();




                    var farm2 = _manager.Details(finca.Id,
                        "Productivity",
                        "Contacts",
                        "Productivity.Plantations",
                        "Productivity.Plantations.FloweringPeriods",
                        "Worker",
                        "OtherActivities",
                        "SoilAnalysis",
                        "SoilTypes",
                        "Fertilizers",
                        "FamilyUnitMembers",
                        "ImpactAssessments",
                        "ImpactAssessments.Answers",
                        "Village",
                        "Village.Municipality",
                        "Village.Municipality.Department");


                    foreach (PlantationDTO p in farm.Productivity.Plantations)
                    {
                        if (p.Density != null && !p.Density.Equals(string.Empty) && Convert.ToDecimal(p.Density) > 0)
                        {
                            PlantationDTO pl = farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault();

                            farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().Density = calcularProdEstimada(p).Density;

                            _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                            //pl.Density = calcularProdEstimada(p).Density;
                        }
                    }


                }

            }
        }

        private PlantationDTO calcularProdEstimada(PlantationDTO plantation)
        {
            try
            {


                List<TablaProduccion> tablaProd = db.ExecuteQuery<TablaProduccion>("TablaProdEstimada_Get").ToList();

                DateTime a = plantation.Age;
                DateTime b = DateTime.Today;

                TimeSpan span = b - a;

                double ageTemp = span.TotalDays / 365;

                int years = Convert.ToInt32(Math.Round(span.TotalDays / 365, 0));

                //int years = Math.Round(span / (1000 * 3600 * 24) / 365);
                if (ageTemp < 2)
                {
                    plantation.EstimatedProduction = "0";
                }
                else
                {
                    int edad = 0;

                    if (ageTemp >= 10)
                    {
                        edad = 10;
                    }
                    else
                    {
                        edad = years;
                    }

                    int conceptoDensidades = Convert.ToDecimal(plantation.Density) <= 6500 ? 1 : 2;

                    string estadoLote = plantation.PlantationStatusId.ToString().ToUpper().Equals("08DB007B-56EE-487E-BB48-129DC9F18A48") ? "B" : "RM";

                    TablaProduccion factor = tablaProd.Where(r => r.Concepto_densidades == conceptoDensidades && r.Edad == edad && r.Estado.Equals(estadoLote)).FirstOrDefault();

                    if (plantation.NumEjeArbLot == 1)
                    {

                        plantation.EstimatedProduction = Math.Round(factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants, 4).ToString();

                    }
                    else
                    {
                        if (conceptoDensidades == 1)
                        {
                            plantation.EstimatedProduction = Math.Round((factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants) * Convert.ToDecimal(1.1), 4).ToString();
                        }
                        else
                        {
                            plantation.EstimatedProduction = Math.Round((factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants), 4).ToString();
                        }


                    }
                }

            }
            catch (Exception ex)
            {

            }
            return plantation;
        }

        private class TablaProduccion
        {
            public decimal Den_Cua_Tri { get; set; }
            public decimal Concepto_densidades { get; set; }
            public string Estado { get; set; }
            public decimal Edad { get; set; }
            public decimal CPS_Cuadro_YARA { get; set; }
            public decimal CPS_Cuadro_Cauca { get; set; }
            public decimal CPS_Kg_ha_Cauca { get; set; }
            public decimal Porc_Producción { get; set; }
            public decimal CPS_KG_Arbol_1_eje_Cauca { get; set; }
            public decimal? CPS_GR_Arbol_2_eje_Cauca { get; set; }
        }
    }
}