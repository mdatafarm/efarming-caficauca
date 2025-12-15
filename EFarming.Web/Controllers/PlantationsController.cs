using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Plantations information
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]
    public class PlantationsController : Controller
    {
        public const int PERPAGE = 10;
        private UnitOfWork db = new UnitOfWork();
        /// <summary>
        /// The _manager
        /// </summary>        
        private IFarmManager _manager;
        private IPlantationTypeManager _plantationtypemanager;
        private IFarmManager _farmManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PlantationsController(FarmManager manager, PlantationTypeManager plantationtypemanager)
        {
            _manager = manager;
            _plantationtypemanager = plantationtypemanager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView With farm</returns>
        public ActionResult Index(Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);

            foreach (PlantationDTO p in farm.Productivity.Plantations)
            {
                if (!string.IsNullOrWhiteSpace(p.Density) &&
                    decimal.TryParse(p.Density.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal densityValue) &&
                    densityValue > 0)
                {
                    PlantationDTO pl = calcularProdEstimada(p);

                    var plantationToUpdate = farm.Productivity.Plantations.FirstOrDefault(x => x.Id == p.Id);
                    if (plantationToUpdate != null)
                    {
                        plantationToUpdate.Density = pl.Density;
                        plantationToUpdate.EstimatedProduction = pl.EstimatedProduction;
                    }

                    _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                }
            }

            // Recargar datos para reflejar los UpdatedAt recién guardados
            farm = _manager.Details(farmId);

            var orderedList = farm.Productivity.Plantations
                .OrderByDescending(p => p.UpdatedAt)
                .ToPagedList(page.Value, PERPAGE);

            ViewBag.PagedPlantations = orderedList;

            return PartialView("~/Views/Plantations/Index.cshtml", farm);
        }
        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="productivityId">The productivity identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with plantation</returns>
        public ActionResult Create(Guid farmId, Guid productivityId, int? page = 1)
        {
            ViewBag.muni = db.Municipalities.OrderBy(x => x.Name).ToList();

            //tipo de labor
            List<IDClaseIdi> List_IDClaseIdiLabor = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiLabor1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor3 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor4 = new IDClaseIdi();

            obj_IDClaseIdiLabor1.Name = "Siembra";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor1);

            obj_IDClaseIdiLabor2.Name = "Nueva siembra";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor2);

            obj_IDClaseIdiLabor3.Name = "Zoca";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor3);

            obj_IDClaseIdiLabor4.Name = "No aplica";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor4);

            ViewBag.Labor = List_IDClaseIdiLabor.OrderBy(x => x.Name).ToList();

            //tipo de lote
            List<IDClaseIdi> List_IDClaseIdiTLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiTLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot3 = new IDClaseIdi();

            obj_IDClaseIdiTLot1.Name = "Tecnificado";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot1);

            obj_IDClaseIdiTLot2.Name = "Tradicional";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot2);

            obj_IDClaseIdiTLot3.Name = "No aplica";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot3);

            ViewBag.TLot = List_IDClaseIdiTLot.OrderBy(x => x.Name).ToList();

            //forma de siembra
            List<IDClaseIdi> List_IDClaseIdiFSiem = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiFSiem1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem3 = new IDClaseIdi();

            obj_IDClaseIdiFSiem1.Name = "Triángulo";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem1);

            obj_IDClaseIdiFSiem2.Name = "Cuadrado";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem2);

            obj_IDClaseIdiFSiem3.Name = "No aplica";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem3);

            ViewBag.FormLot = List_IDClaseIdiFSiem.OrderBy(x => x.Name).ToList();

            //numero de ejes
            List<IDClaseIdi> List_IDClaseIdiNumEjeArbLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot3 = new IDClaseIdi();

            obj_IDClaseIdiNumEjeArbLot1.Name = "1";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot1);

            obj_IDClaseIdiNumEjeArbLot2.Name = "2";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot2);

            obj_IDClaseIdiNumEjeArbLot3.Name = "No aplica";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot3);

            ViewBag.NumEjeArbLot = List_IDClaseIdiNumEjeArbLot.OrderBy(x => x.Name).ToList();

            ViewBag.PageNumber = page.Value;
            var plantation = new PlantationDTO { FarmId = farmId, ProductivityId = productivityId };
            return PartialView("~/Views/Plantations/Create.cshtml", plantation);
        }

        public class IDClaseIdi
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }

        /// <summary>
        /// Creates the specified plantation.
        /// </summary>
        /// <param name="plantation">The plantation.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or plantation</returns>
        [HttpPost]
        public ActionResult Create(PlantationDTO plantation, int? page = 1)
        {

            try
            {
                var numlot = db.Plantations.Where(l => l.ProductivityId == plantation.ProductivityId).ToList().Count();
                plantation.NumberLot = numlot + 1;

                var village = db.Farms.Where(x => x.Id == plantation.ProductivityId).Select(x => x.VillageId);

                plantation.VillageLot = village.FirstOrDefault();

                var Muni = db.Villages.Where(x => x.Id == plantation.VillageLot).Select(x => x.MunicipalityId);
                plantation.MuniLot = Muni.FirstOrDefault();

                if(plantation.PlantationTypeId == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                {
                    plantation = calcularProdEstimada(plantation);
                }
                else
                {

                    if (plantation.PlantationTypeId == new Guid("46496de0-3cd6-4f25-beb7-2446d0f6929d"))
                    {
                        plantation.PlantationVarietyId = new Guid("55959369-0611-4180-adf9-2cf5a602420a");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);

                    }

                    else if (plantation.PlantationTypeId == new Guid("80838f7d-a18e-4f25-aea3-5550ff159eb2"))
                    {
                        plantation.PlantationVarietyId = new Guid("50bd3880-3c7f-4329-855e-2c75f1757b4f");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }

                    else if (plantation.PlantationTypeId == new Guid("9e7b7f6b-03c9-49eb-aafd-5db7f4e8337e"))
                    {
                        plantation.PlantationVarietyId = new Guid("f781c50b-c895-488c-9563-e61227f8ece0");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }

                    else if (plantation.PlantationTypeId == new Guid("067ec55a-7b4b-436b-83df-713b26390929"))
                    {
                        plantation.PlantationVarietyId = new Guid("6279c725-f82f-4057-a970-a64c9531d961");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }
                    else if (plantation.PlantationTypeId == new Guid("b6cf7a3d-6070-4a16-9714-8ce60734d2af"))
                    {
                        plantation.PlantationVarietyId = new Guid("b8b8cf51-22a8-4f35-8277-e48dcdd967cf");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }
                    else if (plantation.PlantationTypeId == new Guid("ee9582c6-46f2-4c99-bca2-a307a09c5973"))
                    {
                        plantation.PlantationVarietyId = new Guid("e6539371-25e7-46f2-a812-615e79938c12");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }
                    else if (plantation.PlantationTypeId == new Guid("260c7669-f515-442e-be64-dcbbc539a991"))
                    {
                        plantation.PlantationVarietyId = new Guid("2fcb2716-213a-45f4-9faa-5e0246bcf57e");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }
                    else if (plantation.PlantationTypeId == new Guid("2cd08524-1be7-49d3-b67d-e7e691001f67"))
                    {
                        plantation.PlantationVarietyId = new Guid("9d198f69-e10a-4e48-a348-5b8408e9badb");
                        plantation.PlantationStatusId = new Guid("f442b168-ed11-4cb4-ac73-9b7125bcc8ff");
                        plantation.TipoLot = ("No aplica");
                        plantation.LabLot = ("No aplica");
                        plantation.FormLot = ("No aplica");
                        plantation.NumEjeArbLot = (3);
                    }




                }

                var farm = _manager.Details(plantation.FarmId);
                farm.Productivity.Plantations.Add(plantation);
                _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                farm = _manager.Details(plantation.FarmId);
                var PagedPlantations = farm.Productivity.Plantations.ToPagedList(page.Value, PERPAGE);
                foreach (var plant in PagedPlantations)
                {
                    plant.PlantationType = _plantationtypemanager.Get(plant.PlantationTypeId);
                }

                ViewBag.PagedPlantations = PagedPlantations;

                return PartialView("~/Views/Plantations/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Plantations/Create.cshtml", plantation);
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

                    if (factor == null || plantation.NumberOfPlants <= 0)
                    {
                        plantation.EstimatedProduction = "0";
                        return plantation;
                    }

                    else
                    {

                        if (plantation.NumEjeArbLot == 1)
                        {

                            plantation.EstimatedProduction = Math.Round(factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants, 4).ToString();

                        }
                        else
                        {
                            if (conceptoDensidades == 1)
                            {
                                if (factor != null)
                                {
                                    plantation.EstimatedProduction = Math.Round((factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants) * Convert.ToDecimal(1.1), 4).ToString();
                                }

                            }
                            else
                            {
                                plantation.EstimatedProduction = Math.Round((factor.CPS_KG_Arbol_1_eje_Cauca * plantation.NumberOfPlants), 4).ToString();
                            }


                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error calculando producción estimada", ex);
            }
            return plantation;
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with Plantation</returns>
        public ActionResult Edit(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.muni = db.Municipalities.OrderBy(x => x.Name).ToList();
            ViewBag.vill = db.Villages.OrderBy(x => x.Name).ToList();

            //tipo de labor
            List<IDClaseIdi> List_IDClaseIdiLabor = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiLabor1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor3 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor4 = new IDClaseIdi();

            obj_IDClaseIdiLabor1.Name = "Siembra";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor1);

            obj_IDClaseIdiLabor3.Name = "Zoca";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor3);

            obj_IDClaseIdiLabor4.Name = "No aplica";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor4);

            ViewBag.Labor = List_IDClaseIdiLabor.OrderBy(x => x.Name).ToList();

            //tipo de lote
            List<IDClaseIdi> List_IDClaseIdiTLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiTLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot3 = new IDClaseIdi();

            obj_IDClaseIdiTLot1.Name = "Tecnificado";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot1);

            obj_IDClaseIdiTLot2.Name = "Tradicional";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot2);

            obj_IDClaseIdiTLot3.Name = "No aplica";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot3);

            ViewBag.TLot = List_IDClaseIdiTLot.OrderBy(x => x.Name).ToList();

            //forma de siembra
            List<IDClaseIdi> List_IDClaseIdiFSiem = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiFSiem1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem3 = new IDClaseIdi();

            obj_IDClaseIdiFSiem1.Name = "Triángulo";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem1);

            obj_IDClaseIdiFSiem2.Name = "Cuadrado";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem2);

            obj_IDClaseIdiFSiem3.Name = "No aplica";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem3);

            ViewBag.FormLot = List_IDClaseIdiFSiem.OrderBy(x => x.Name).ToList();

            //numero de ejes
            List<IDClaseIdi> List_IDClaseIdiNumEjeArbLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot3 = new IDClaseIdi();

            obj_IDClaseIdiNumEjeArbLot1.Name = "1";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot1);

            obj_IDClaseIdiNumEjeArbLot2.Name = "2";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot2);

            obj_IDClaseIdiNumEjeArbLot3.Name = "No aplica";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot3);

            ViewBag.NumEjeArbLot = List_IDClaseIdiNumEjeArbLot.OrderBy(x => x.Name).ToList();

            ViewBag.PageNumber = page.Value;
            var farm = _manager.Details(farmId);
            var plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(id));


            //obtener la vereda escogida
            var villageID = db.Villages.Where(x => x.Id == plantation.VillageLot).FirstOrDefault();

            //mandar a la vista la vereda seleccionada
            ViewBag.SelectedVill = villageID.Id;

            //llenar viewbag con las veredas del municipio seleccionado
            ViewBag.vill = db.Villages.Where(x => x.MunicipalityId == villageID.MunicipalityId).OrderBy(x => x.Name).ToList();

            //mandar a la vista el municipio seleccionado
            ViewBag.SelectedMuni = db.Municipalities.Where(x => x.Id == villageID.MunicipalityId).Select(x => x.Id).FirstOrDefault();
            return PartialView("~/Views/Plantations/Edit.cshtml", plantation);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="plantation">The plantation.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or Plantation</returns>
        //[HttpPost]
        //public ActionResult Edit(Guid id, PlantationDTO plantation, int? page = 1)
        //{
        //    try
        //    {
        //        var village = db.Farms.Where(x => x.Id == plantation.ProductivityId).Select(x => x.VillageId);

        //        plantation.VillageLot = village.FirstOrDefault();

        //        var Muni = db.Villages.Where(x => x.Id == plantation.VillageLot).Select(x => x.MunicipalityId);
        //        plantation.MuniLot = Muni.FirstOrDefault();

        //        plantation = calcularProdEstimada(plantation);

        //        var farm = _manager.Details(plantation.FarmId);
        //        var toRemove = farm.Productivity.Plantations.First(p => p.Id.Equals(id));
        //        farm.Productivity.Plantations.Remove(toRemove);
        //        farm.Productivity.Plantations.Add(plantation);
        //        _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
        //        farm = _manager.Details(plantation.FarmId);
        //        ViewBag.PagedPlantations = farm.Productivity.Plantations.ToPagedList(page.Value, PERPAGE);
        //        return PartialView("~/Views/Plantations/Index.cshtml", farm);
        //    }
        //    catch
        //    {
        //        return PartialView("~/Views/Plantations/Edit.cshtml", plantation);
        //    }
        //}

        [HttpPost]
        public ActionResult Edit(Guid id, PlantationDTO plantation, int? page = 1)
        {
            try
            {
                var village = db.Farms.Where(x => x.Id == plantation.ProductivityId).Select(x => x.VillageId).FirstOrDefault();
                plantation.VillageLot = village;

                var muni = db.Villages.Where(x => x.Id == plantation.VillageLot).Select(x => x.MunicipalityId).FirstOrDefault();
                plantation.MuniLot = muni;

                plantation = calcularProdEstimada(plantation);

                var farm = _manager.Details(plantation.FarmId);

                var existingPlantation = farm.Productivity.Plantations.FirstOrDefault(p => p.Id.Equals(id));

                if (existingPlantation != null)
                {
                    existingPlantation.PlantationType = plantation.PlantationType;
                    existingPlantation.PlantationVariety = plantation.PlantationVariety;
                    existingPlantation.Hectares = plantation.Hectares;
                    existingPlantation.Density = plantation.Density;
                    existingPlantation.EstimatedProduction = plantation.EstimatedProduction;
                    existingPlantation.EstimatedProductionManual = plantation.EstimatedProductionManual;
                    existingPlantation.NumberOfPlants = plantation.NumberOfPlants;
                    existingPlantation.Age = plantation.Age;
                    existingPlantation.VillageLot = plantation.VillageLot;
                    existingPlantation.MuniLot = plantation.MuniLot;
                    existingPlantation.UpdatedAt = DateTime.Now;

                    // Buscar el lote real en la tabla de la base
                    var plantationDb = db.Plantations.FirstOrDefault(p => p.Id == id);
                    if (plantationDb != null)
                    {
                        plantationDb.PlantationTypeId = plantation.PlantationTypeId;
                        plantationDb.PlantationVarietyId = plantation.PlantationVarietyId;
                        plantationDb.Hectares = plantation.Hectares;
                        plantationDb.Density = plantation.Density;
                        plantationDb.EstimatedProduction = plantation.EstimatedProduction;
                        plantationDb.EstimatedProductionManual = plantation.EstimatedProductionManual;
                        plantationDb.NumberOfPlants = plantation.NumberOfPlants;
                        plantationDb.Age = plantation.Age;
                        plantationDb.VillageLot = plantation.VillageLot;
                        plantationDb.MuniLot = plantation.MuniLot;
                        plantationDb.UpdatedAt = DateTime.Now;
                    }

                    db.SaveChanges();
                }

                _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);

                farm = _manager.Details(plantation.FarmId);
                ViewBag.PagedPlantations = farm.Productivity.Plantations.ToPagedList(page.Value, PERPAGE);

                return PartialView("~/Views/Plantations/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Plantations/Edit.cshtml", plantation);
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with plantation</returns>
        public ActionResult Delete(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var plantation = _manager.Details(farmId).Productivity.Plantations.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/Plantations/Delete.cshtml", plantation);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="plantation">The plantation.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or plantation</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, PlantationDTO plantation, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(plantation.FarmId);
                var toRemove = farm.Productivity.Plantations.First(sa => sa.Id.Equals(id));
                farm.Productivity.Plantations.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                farm = _manager.Details(plantation.FarmId);

                decimal totalHectareas = 0;

                if (farm.Productivity.Plantations.Count > 0)
                {
                    foreach (PlantationDTO p in farm.Productivity.Plantations)
                    {
                        //if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                        //{
                        //    totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                        //}

                        totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                    }
                }

                decimal infHct = farm.Productivity.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.InfrastructureHectares);
                decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
                decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
                //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

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

                ViewBag.PagedPlantations = farm.Productivity.Plantations.ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Plantations/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Plantations/Delete.cshtml", plantation);
            }
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
