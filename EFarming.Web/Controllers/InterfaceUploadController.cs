using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.DAL;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EFarming.DAL.UnitOfWork;

namespace EFarming.Web.Controllers
{
    public class InterfaceUploadController : Controller
    {
        private UnitOfWork db = new UnitOfWork();
        DriverDataAccess _driver = new DriverDataAccess();

        private IFarmManager _manager;

        public InterfaceUploadController(FarmManager manager)
        {
            _manager = manager;
        }

        // GET: InterfaceUpload
        public ActionResult IndexUploads()
        {
            return View("InterfaceFarms");
        }

        public ActionResult IndexInterfaceFamily()
        {
            return View("InterfaceFamily");
        }

        public ActionResult IndexInterfacePlantations()
        {
            return View("InterfacePlantations");
        }

        public class Errores
        {
            public string type { get; set; }
            public string Description { get; set; }
            public string LineNumber { get; set; }
            public string Source { get; set; }
            public string Procedure { get; set; }
            public int state { get; set; }

        }

        public class ObjM
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Listas
        {
            public string Name { get; set; }
            public string Origin { get; set; }

        }

        public class ObjV
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid Muni { get; set; }

        }

        public class IDClaseIdi
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }

        [HttpPost]
        public async Task<ActionResult> UploadData(HttpPostedFileBase excelfile, int type)
        {
            List<Errores> ListError = new List<Errores>();
            ViewBag.ListaE = null;
            ViewBag.F_Error = null;
            ViewBag.F_Bad = null;
            ViewBag.F_Good = null;

            var Munici = db.Municipalities.Where(x => x.DeletedAt == null).ToList();
            var Villages = db.Villages.Where(x => x.DeletedAt == null).ToList();

            List<ObjM> Formated = new List<ObjM>();

            foreach (var item in Munici)
            {
                ObjM mnp = new ObjM();
                mnp.Id = item.Id;
                mnp.Name = RemoveDiacritics(item.Name.ToUpper());
                Formated.Add(mnp);
            }

            List<ObjV> FormatedV = new List<ObjV>();

            foreach (var item in Villages)
            {
                ObjV vil = new ObjV();
                vil.Id = item.Id;
                vil.Name = RemoveDiacritics(item.Name.ToUpper());
                vil.Muni = item.MunicipalityId;
                FormatedV.Add(vil);
            }

            Errores er = new Errores();
            Errores er1 = new Errores();

            db.Database.CommandTimeout = 999999999;

            if (excelfile != null)
            {
                //List<List<object>> loadResults = new List<List<object>>();
                if (excelfile.ContentLength > 0)
                {
                    string savedFileName1 = "~/Content/InterfaceTraceability/" + excelfile.FileName;
                    excelfile.SaveAs(Server.MapPath(savedFileName1));

                    DataTable dtTemporal = ConvertCSVtoDataTable(Server.MapPath(savedFileName1));
                    var dato = dtTemporal.Columns.Count;
                    string ext = Path.GetExtension(savedFileName1);
                    DateTime TodayTo = DateTime.Now;
                    var Datails = "";

                    //var Year = DateTime.Now.Year;
                    //var Month = DateTime.Now.Month;
                    //var day = DateTime.Now.Day;

                    //var today = Year +'-'+ Month + '-' + day;

                    if (type == 3)
                    {
                        if (dato != 19 && ext == ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 19";
                            ListError.Add(er);
                            ViewBag.ListaE = ListError;
                            return View("InterfacePlantations", ViewBag.ListaE);
                        }
                        else if (dato != 19 && ext != ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 19";
                            ListError.Add(er);

                            er1.type = "Extension";
                            er1.Description = "El archivo que esta intentando subir tiene la extension " + ext + ", solo se puede subir archivos con extension .csv delimitado por ; (punto y coma)";
                            ListError.Add(er1);

                            ViewBag.ListaE = ListError;

                            return View("InterfacePlantations", ViewBag.ListaE);
                        }
                        else
                        {

                            List<Plantation_Error> FieldsPL = new List<Plantation_Error>();

                            int count = 2;
                            int C_Bad = 0;
                            int C_Good = 0;

                            DataTable data_Good = dtTemporal.Clone();
                            DataTable data_Bad = dtTemporal.Clone();

                            var VeriT_Plant1 = db.PlantationType.Where(x => x.DeletedAt == null).ToList();
                            var VeriV_Plant1 = db.PlantationVarieties.Where(x => x.DeletedAt == null).ToList();

                            List<PlantationsT> PT = new List<PlantationsT>();
                            foreach (var item in VeriT_Plant1)
                            {
                                PlantationsT PTypes = new PlantationsT();
                                PTypes.Id = item.Id;
                                PTypes.Name = RemoveDiacritics(item.Name);
                                PT.Add(PTypes);
                            }

                            List<PlantationsT> PTV = new List<PlantationsT>();
                            foreach (var item in VeriV_Plant1)
                            {
                                PlantationsT PTypesV = new PlantationsT();
                                PTypesV.Id = item.Id;
                                PTypesV.Name = RemoveDiacritics(item.Name);
                                PTypesV.PlantationTypeId = item.PlantationTypeId;
                                PTV.Add(PTypesV);
                            }

                            //----------------------------------------------------------------------------
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

                            List<IDClaseIdi> List_LL = new List<IDClaseIdi>();
                            foreach (var item in List_IDClaseIdiLabor)
                            {
                                IDClaseIdi Lab_L = new IDClaseIdi();
                                Lab_L.Id = item.Id;
                                Lab_L.Name = RemoveDiacritics(item.Name);
                                List_LL.Add(Lab_L);
                            }
                            //----------------------------------------------------------------------------

                            //tipo de lote
                            List<IDClaseIdi> List_IDClaseIdiTLot = new List<IDClaseIdi>();
                            IDClaseIdi obj_IDClaseIdiTLot1 = new IDClaseIdi();
                            IDClaseIdi obj_IDClaseIdiTLot2 = new IDClaseIdi();

                            obj_IDClaseIdiTLot1.Name = "Tecnificado";
                            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot1);

                            obj_IDClaseIdiTLot2.Name = "Tradicional";
                            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot2);

                            List<IDClaseIdi> List_TL = new List<IDClaseIdi>();
                            foreach (var item in List_IDClaseIdiTLot)
                            {
                                IDClaseIdi Ti_L = new IDClaseIdi();
                                Ti_L.Id = item.Id;
                                Ti_L.Name = RemoveDiacritics(item.Name);
                                List_TL.Add(Ti_L);
                            }
                            //----------------------------------------------------------------------------


                            //forma de siembra
                            List<IDClaseIdi> List_IDClaseIdiFSiem = new List<IDClaseIdi>();
                            IDClaseIdi obj_IDClaseIdiFSiem1 = new IDClaseIdi();
                            IDClaseIdi obj_IDClaseIdiFSiem2 = new IDClaseIdi();

                            obj_IDClaseIdiFSiem1.Name = "Triángulo";
                            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem1);

                            obj_IDClaseIdiFSiem2.Name = "Cuadrado";
                            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem2);

                            ViewBag.FormLot = List_IDClaseIdiFSiem.OrderBy(x => x.Name).ToList();

                            List<IDClaseIdi> List_FS = new List<IDClaseIdi>();
                            foreach (var item in List_IDClaseIdiFSiem)
                            {
                                IDClaseIdi FS_L = new IDClaseIdi();
                                FS_L.Id = item.Id;
                                FS_L.Name = RemoveDiacritics(item.Name);
                                List_FS.Add(FS_L);
                            }
                            //----------------------------------------------------------------------------


                            //numero de ejes
                            List<IDClaseIdi> List_IDClaseIdiNumEjeArbLot = new List<IDClaseIdi>();
                            IDClaseIdi obj_IDClaseIdiNumEjeArbLot1 = new IDClaseIdi();
                            IDClaseIdi obj_IDClaseIdiNumEjeArbLot2 = new IDClaseIdi();

                            obj_IDClaseIdiNumEjeArbLot1.Name = "1";
                            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot1);

                            obj_IDClaseIdiNumEjeArbLot2.Name = "2";
                            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot2);

                            List<IDClaseIdi> List_NE = new List<IDClaseIdi>();
                            foreach (var item in List_IDClaseIdiNumEjeArbLot)
                            {
                                IDClaseIdi NE_L = new IDClaseIdi();
                                NE_L.Id = item.Id;
                                NE_L.Name = RemoveDiacritics(item.Name);
                                List_NE.Add(NE_L);
                            }
                            //----------------------------------------------------------------------------


                            foreach (DataRow r in dtTemporal.Rows)
                            {
                                Plantation_Error DatosMalosPlant = new Plantation_Error();
                                DatosMalosPlant.Fila = count;

                                string C_F3 = r[0].ToString();

                                bool C_finca = VerifyNull(r[0].ToString());
                                if (C_finca == true)
                                {
                                    DatosMalosPlant.Codigo_F = "Campo Obligatorio";
                                    r[0] = DatosMalosPlant.Codigo_F;
                                    C_Bad++;
                                }
                                else
                                {
                                    //validar codigo de finca
                                    bool IsNumber = VerifyNumber(r[0].ToString());
                                    if (IsNumber == true)
                                    {
                                        var CodeV = r[0].ToString().Trim();
                                        var VeriCode = db.Farms.Where(x => x.Code == CodeV).ToList();

                                        if (VeriCode.Count() > 0)
                                        {
                                            if (VeriCode.Count() > 1)
                                            {
                                                DatosMalosPlant.Codigo_F = "El codigo: " + CodeV + " se encontro mas de una vez en la base de datos.";
                                                r[0] = DatosMalosPlant.Codigo_F;
                                                C_Bad++;
                                            }
                                            else if (VeriCode.Count() == 1)
                                            {
                                                foreach (var item in VeriCode)
                                                {
                                                    r[0] = item.Id.ToString();
                                                    C_Good++;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            DatosMalosPlant.Codigo_F = "El codigo: " + CodeV + " no se encontro en la base de datos.";
                                            r[0] = DatosMalosPlant.Codigo_F;
                                            C_Bad++;
                                        }
                                    }
                                    else
                                    {
                                        DatosMalosPlant.Codigo_F = "Este campo no puede contener letras";
                                        r[0] = DatosMalosPlant.Codigo_F;
                                        C_Bad++;
                                    }
                                }

                                //Validar Hectareas
                                decimal hectareas1 = 0;
                                bool Hectareas = VerifyNull(r[1].ToString());
                                if (Hectareas == true)
                                {
                                    DatosMalosPlant.Hectareas = "Campo Obligatorio";
                                    r[1] = DatosMalosPlant.Hectareas;
                                    C_Bad++;
                                }
                                else
                                {
                                    bool HectareasV = Verifyfloat(r[1].ToString());
                                    if (HectareasV == false)
                                    {
                                        DatosMalosPlant.Hectareas = "Formato erroneo, este campo debe ser un dato numerico separado por coma ej: 1,6055";
                                        r[1] = DatosMalosPlant.Hectareas;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        hectareas1 = Convert.ToDecimal(r[1].ToString());
                                        C_Good++;
                                    }
                                }

                                //Verificar estado de plantacion
                                bool E_Plant = VerifyNull(r[3].ToString());
                                if (E_Plant == false)
                                {
                                    string RemovE_Plant = RemoveDiacritics(r[3].ToString().ToUpper());
                                    var VeriE_Plant = db.PlantationStatuses.Where(x => x.Name.Contains(RemovE_Plant) && x.DeletedAt == null).ToList();

                                    if (VeriE_Plant == null || VeriE_Plant.Count == 0)
                                    {
                                        DatosMalosPlant.Est_Plantacion = "El estado de plantación : '" + r[3].ToString() + "' no se encontro en la base de datos";
                                        r[3] = DatosMalosPlant.Est_Plantacion;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (VeriE_Plant.Count() > 1)
                                        {
                                            var VeriE_PlantE = VeriE_Plant.Where(x => x.Name == RemovE_Plant).ToList();

                                            if (VeriE_PlantE.Count() == 0)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countR = 1;

                                                foreach (var item in VeriE_Plant)
                                                {
                                                    if (countR == VeriE_Plant.Count())
                                                    {
                                                        if (countR == 2)
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + item.Name + ".";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (countR == 1)
                                                        {
                                                            TypeOwners = item.Name;
                                                        }
                                                        else
                                                        {
                                                            if (countR <= 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                            }
                                                        }
                                                        countR++;
                                                    }
                                                }

                                                DatosMalosPlant.Est_Plantacion = "Se encontro multiples resultados para el estado de plantacion: '" + r[6].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[3] = DatosMalosPlant.Est_Plantacion;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                string IdE_Plant = VeriE_Plant.Select(x => x.Id.ToString()).FirstOrDefault();
                                                r[3] = IdE_Plant;
                                                C_Good++;
                                            }
                                        }
                                        else if (VeriE_Plant.Count() == 1)
                                        {
                                            string IdE_Plant = VeriE_Plant.Select(x => x.Id.ToString()).FirstOrDefault();
                                            r[3] = IdE_Plant;
                                            C_Good++;
                                        }

                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Est_Plantacion = "Campo Obligatorio";
                                    r[3] = DatosMalosPlant.Est_Plantacion;
                                    C_Bad++;
                                }


                                //Verificar tipo de plantacion
                                string TPlantG = r[4].ToString();
                                bool T_Plant = VerifyNull(r[4].ToString());
                                if (T_Plant == false)
                                {
                                    string RemovT_Plant = RemoveDiacritics(r[4].ToString().ToUpper());

                                    var VeriT_Plant = PT.Where(x => x.Name.Contains(RemovT_Plant)).ToList();

                                    if (VeriT_Plant == null || VeriT_Plant.Count == 0)
                                    {
                                        DatosMalosPlant.Tip_Plantacion = "El tipo de plantación : '" + r[4].ToString() + "' no se encontro en la base de datos";
                                        r[4] = DatosMalosPlant.Tip_Plantacion;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (VeriT_Plant.Count() > 1)
                                        {
                                            var VeriE_PlantE = VeriT_Plant.Where(x => x.Name == RemovT_Plant).ToList();

                                            if (VeriE_PlantE.Count() == 0)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countR = 1;

                                                foreach (var item in VeriT_Plant)
                                                {
                                                    if (countR == VeriE_PlantE.Count())
                                                    {
                                                        if (countR == 2)
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + item.Name + ".";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (countR == 1)
                                                        {
                                                            TypeOwners = item.Name;
                                                        }
                                                        else
                                                        {
                                                            if (countR <= 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                            }
                                                        }
                                                        countR++;
                                                    }
                                                }

                                                DatosMalosPlant.Tip_Plantacion = "Se encontro multiples resultados para el tipo de plantacion: '" + r[4].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[4] = DatosMalosPlant.Tip_Plantacion;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                string IdT_Plant = VeriE_PlantE.Select(x => x.Id.ToString()).FirstOrDefault();
                                                r[4] = IdT_Plant;
                                                C_Good++;
                                            }
                                        }
                                        else if (VeriT_Plant.Count() == 1)
                                        {
                                            string IdT_Plant = VeriT_Plant.Select(x => x.Id.ToString()).FirstOrDefault();
                                            r[4] = IdT_Plant;
                                            C_Good++;
                                        }

                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Tip_Plantacion = "Campo Obligatorio";
                                    r[4] = DatosMalosPlant.Tip_Plantacion;
                                    C_Bad++;
                                }

                                //Validar Edad
                                bool F_Naci = VerifyNull(r[2].ToString());
                                Guid guidOutput2 = Guid.Empty;
                                bool isValid2 = Guid.TryParse(r[4].ToString(), out guidOutput2);
                                if (F_Naci == true)
                                {
                                    if (isValid2 == true)
                                    {
                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            DatosMalosPlant.Edad = "Campo Obligatorio";
                                            r[2] = DatosMalosPlant.Edad;
                                            C_Bad++;
                                        }
                                        else
                                        {

                                            r[2] = TodayTo.ToString("yyyyMMdd");
                                            C_Good++;
                                        }
                                    }
                                    else
                                    {
                                        C_Bad++;
                                    }

                                }
                                else
                                {
                                    if (isValid2 == true)
                                    {
                                        var date = r[2].ToString().TrimEnd().TrimStart();
                                        DateTime dateValue;

                                        if (DateTime.TryParseExact(date, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                                        {
                                            r[2] = r[2].ToString().TrimEnd().TrimStart().Replace("-", "");
                                            C_Good++;
                                        }
                                        else
                                        {
                                            DatosMalosPlant.Edad = "El formato de la fecha: " + date + " debe ser del tipo 'yyyy-MM-dd'.";
                                            r[2] = DatosMalosPlant.Edad;
                                            C_Bad++;
                                        }
                                    }
                                    else
                                    {
                                        C_Bad++;
                                    }

                                }

                                //Verificar Variedad plantacion PTV
                                bool V_Plant = VerifyNull(r[5].ToString());
                                if (V_Plant == false)
                                {
                                    string RemovV_Plant = RemoveDiacritics(r[5].ToString().ToUpper());

                                    Guid guidOutput = Guid.Empty;
                                    bool isValid = Guid.TryParse(r[4].ToString(), out guidOutput);

                                    var VeriV_Plant = PTV;

                                    if (isValid == false)
                                    {
                                        DatosMalosPlant.Variedad = "No se pude verificar el tipo de variedad: " + r[5].ToString() + " por que no se encontro el id para el tipo de plantacion: " + TPlantG;
                                        r[5] = DatosMalosPlant.Variedad;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        VeriV_Plant = PTV.Where(x => x.Name.Contains(RemovV_Plant)).ToList();
                                        var VeriV_PlantType = VeriV_Plant.Where(x => x.PlantationTypeId == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5")).ToList();

                                        if (VeriV_Plant == null || VeriV_Plant.Count == 0)
                                        {
                                            DatosMalosPlant.Variedad = "El tipo de variedad : '" + r[5].ToString() + "' no se encontro en la base de datos";
                                            r[5] = DatosMalosPlant.Variedad;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            if (VeriV_PlantType == null || VeriV_PlantType.Count == 0)
                                            {
                                                DatosMalosPlant.Variedad = "El tipo de variedad : '" + r[5].ToString() + "' existe, pero no esta asociada al tipo de plantacion: '" + TPlantG + "'";
                                                r[5] = DatosMalosPlant.Variedad;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                if (VeriV_Plant.Count() > 1)
                                                {
                                                    var VeriV_PlantE = VeriV_Plant.Where(x => x.Name == RemovV_Plant).ToList();

                                                    if (VeriV_PlantE.Count() == 0)
                                                    {
                                                        string TypeOwners = "";
                                                        string TypeOwnersE = ", ";

                                                        int countR = 1;

                                                        foreach (var item in VeriV_Plant)
                                                        {
                                                            if (countR == VeriV_PlantE.Count())
                                                            {
                                                                if (countR == 2)
                                                                {
                                                                    TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                                }
                                                                else
                                                                {
                                                                    TypeOwners = TypeOwners + item.Name + ".";
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (countR == 1)
                                                                {
                                                                    TypeOwners = item.Name;
                                                                }
                                                                else
                                                                {
                                                                    if (countR <= 2)
                                                                    {
                                                                        TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                                    }
                                                                    else
                                                                    {
                                                                        TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                                    }
                                                                }
                                                                countR++;
                                                            }
                                                        }

                                                        DatosMalosPlant.Variedad = "Se encontro multiples resultados para el tipo de variedad: '" + r[5].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                        r[5] = DatosMalosPlant.Variedad;
                                                        C_Bad++;
                                                    }
                                                    else
                                                    {
                                                        string IdV_Plant = VeriV_PlantE.Select(x => x.Id.ToString()).FirstOrDefault();
                                                        r[5] = IdV_Plant;
                                                        C_Good++;
                                                    }
                                                }
                                                else if (VeriV_Plant.Count() == 1)
                                                {
                                                    string IdV_Plant = VeriV_Plant.Select(x => x.Id.ToString()).FirstOrDefault();
                                                    r[5] = IdV_Plant;
                                                    C_Good++;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Variedad = "Campo Obligatorio";
                                    r[5] = DatosMalosPlant.Variedad;
                                    C_Bad++;
                                }

                                //Verificar Distancia entre arboles
                                decimal DEA_OPE = 0;
                                bool DEA = VerifyNull(r[6].ToString());
                                bool DEA_V = Verifyfloat(r[6].ToString());
                                if (DEA == true)
                                {
                                    if (isValid2 == true)
                                    {
                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            DatosMalosPlant.Distancia_ARB = "Campo Obligatorio";
                                            r[6] = DatosMalosPlant.Distancia_ARB;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            r[6] = 0;
                                            C_Good++;
                                        }
                                    }

                                }
                                else
                                {
                                    if (isValid2 == true)
                                    {
                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            if (DEA_V == false)
                                            {
                                                DatosMalosPlant.Distancia_ARB = "Formato erroneo, este campo debe ser un dato numerico separado por coma ej: 1,6055";
                                                r[6] = DatosMalosPlant.Distancia_ARB;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                DEA_OPE = Convert.ToDecimal(r[6].ToString());
                                                C_Good++;
                                            }

                                        }
                                        else
                                        {
                                            DEA_OPE = 0;
                                            C_Good++;
                                        }
                                    }
                                }

                                //Verificar Distancia entre surcos
                                decimal Density = 0;
                                bool DES = VerifyNull(r[7].ToString());
                                if (DES == true)
                                {
                                    if (isValid2 == true)
                                    {
                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            DatosMalosPlant.Distancia_SUR = "Campo Obligatorio";
                                            r[7] = DatosMalosPlant.Distancia_SUR;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            r[7] = 0;
                                            r[9] = 0;
                                            C_Good++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (isValid2 == true)
                                    {

                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            bool DES_V = Verifyfloat(r[7].ToString());

                                            if (DES_V == false)
                                            {
                                                DatosMalosPlant.Distancia_SUR = "Formato erroneo, este campo debe ser un dato numerico separado por coma ej: 1,6055";
                                                r[7] = DatosMalosPlant.Distancia_SUR;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                if (count == 13968)
                                                {
                                                    var dato22 = 0;
                                                }
                                                //Verificar Densidad
                                                //verifica si la distancia entre arboles sea un float
                                                if (DEA_V == true)
                                                {
                                                    if (r[6].ToString() == "0" || r[7].ToString() == "0" || r[6].ToString() == "0,00" || r[7].ToString() == "0,00")
                                                    {
                                                        Density = 0;
                                                    }
                                                    else
                                                    {
                                                        Density = (10000 / (Convert.ToDecimal(r[6].ToString()) * Convert.ToDecimal(r[7].ToString())));
                                                        r[9] = Math.Round(Density, 2);
                                                        C_Good++;
                                                    }

                                                }
                                                else
                                                {
                                                    DatosMalosPlant.Densidad = "La distancia entre arboles tiene un Formato erroneo, el formato de la distancia entre arboles debe ser un dato numerico separado por coma ej: 1,6055, al no tener un formato correcto no se puede realizar la operacion de la densidad.";
                                                    r[9] = DatosMalosPlant.Distancia_SUR;
                                                    C_Bad++;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            r[7] = 0;
                                            r[9] = 0;
                                            C_Good++;
                                        }
                                    }

                                }

                                //Validar Produccion Estimada
                                bool Pro_EST = VerifyNull(r[8].ToString());
                                if (Pro_EST == true)
                                {
                                    r[8] = 0;
                                    C_Good++;
                                }
                                else
                                {
                                    if (isValid2 == true)
                                    {
                                        if (new Guid(r[4].ToString()) == new Guid("d221bec9-5f73-43a0-9ebf-16417f5674f5"))
                                        {
                                            bool Pro_ESTV = Verifyfloat(r[8].ToString());
                                            if (Pro_ESTV == false)
                                            {
                                                DatosMalosPlant.P_Estimada = "Formato erroneo, este campo debe ser un dato numerico separado por coma ej: 1,6055";
                                                r[8] = DatosMalosPlant.P_Estimada;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                C_Good++;
                                            }
                                        }
                                        else
                                        {
                                            r[8] = 0;
                                            C_Good++;
                                        }
                                    }

                                }

                                //validar numero de plantas
                                decimal N_Plants = 0;
                                if (Density != 0)
                                {
                                    N_Plants = (Math.Round(hectareas1, 3) * Density);
                                    r[10] = Convert.ToInt32(Math.Round(N_Plants, 2));
                                    C_Good++;
                                }
                                else
                                {
                                    r[10] = 0;
                                    C_Good++;
                                }

                                //Validar numero del lote
                                bool C_Lote = VerifyNull(r[11].ToString());
                                if (C_Lote == true)
                                {
                                    DatosMalosPlant.Codigo_lot = "Campo Obligatorio";
                                    r[11] = DatosMalosPlant.Codigo_lot;
                                    C_Bad++;
                                }
                                else
                                {
                                    bool IsNumber = VerifyNumber(r[11].ToString());
                                    if (IsNumber == true)
                                    {
                                        r[11] = r[11].ToString();
                                        C_Good++;
                                    }
                                    else
                                    {
                                        DatosMalosPlant.Codigo_lot = "Este campo no puede contener letras";
                                        r[11] = DatosMalosPlant.Codigo_lot;
                                        C_Bad++;
                                    }
                                }

                                //Validar municipio y vereda
                                bool Muni = VerifyNull(r[12].ToString());
                                bool Vill = VerifyNull(r[13].ToString());
                                if (Muni == true && Vill == true)
                                {
                                    DatosMalosPlant.Muni_lot = "Campo Obligatorio";
                                    r[12] = DatosMalosPlant.Muni_lot;

                                    DatosMalosPlant.Vill_lot = "Campo Obligatorio";
                                    r[13] = DatosMalosPlant.Vill_lot;

                                    C_Bad++;
                                }
                                else if (Muni == false && Vill == true)
                                {
                                    DatosMalosPlant.Vill_lot = "Campo Obligatorio";
                                    r[13] = DatosMalosPlant.Vill_lot;
                                    C_Bad++;
                                }
                                else if (Muni == true && Vill == false)
                                {
                                    DatosMalosPlant.Muni_lot = "Campo Obligatorio";
                                    r[12] = DatosMalosPlant.Muni_lot;
                                    C_Bad++;
                                }
                                else
                                {
                                    string RemovMuniT = RemoveDiacritics(r[12].ToString().ToUpper());
                                    
                                    var ExistMuni = Formated.Where(x => x.Name.Contains(RemovMuniT)).FirstOrDefault();

                                    if (ExistMuni == null)
                                    {
                                        DatosMalosPlant.Muni_lot = "El municipio " + r[12].ToString() + " no se encontro en la base de datos";
                                        r[12] = DatosMalosPlant.Muni_lot;

                                        DatosMalosPlant.Vill_lot = "No se puede verificar la vereda por que el municipio no se encontro";
                                        r[13] = DatosMalosPlant.Vill_lot;

                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovVillT = RemoveDiacritics(r[13].ToString().ToUpper());
                                        

                                        var FormatedVV = FormatedV.Where(x => x.Name.Contains(RemovVillT)).ToList();

                                        if (FormatedVV == null)
                                        {
                                            DatosMalosPlant.Vill_lot = "La vereda + " + r[13].ToString() + "+ no se encontro en la base de datos";
                                            r[13] = DatosMalosPlant.Vill_lot;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            var match = FormatedVV.Where(x => x.Muni == ExistMuni.Id).FirstOrDefault();

                                            if (match == null || match.Id == new Guid("00000000-0000-0000-0000-000000000000"))
                                            {
                                                DatosMalosPlant.Vill_lot = "La vereda: " + r[13].ToString() + " no esta asociada al municipio: " + r[12].ToString();
                                                r[13] = DatosMalosPlant.Vill_lot;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[12] = match.Muni.ToString();
                                                r[13] = match.Id.ToString();
                                                C_Good++;
                                            }
                                        }
                                    }
                                }

                                //Verificar nombre del lote
                                bool Nom_lot = VerifyNull(r[14].ToString());
                                if (Nom_lot == false)
                                {
                                    r[14] = r[14].ToString();
                                    C_Good++;
                                }
                                else
                                {
                                    DatosMalosPlant.Nomb_lot = "Campo Obligatorio";
                                    r[14] = DatosMalosPlant.Nomb_lot;
                                    C_Bad++;
                                }

                                //Verificar labor del lote
                                bool Lab_lot = VerifyNull(r[15].ToString());
                                if (Lab_lot == false)
                                {
                                    bool IsNumber = VerifyNumber(r[15].ToString());
                                    if (IsNumber == true)
                                    {
                                        DatosMalosPlant.Labor_lot = "Este campo no puede contener números.";
                                        r[15] = DatosMalosPlant.Labor_lot;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovLab_lot = RemoveDiacritics(r[15].ToString().ToUpper());

                                        var VeriLab_lot = List_LL.Where(x => x.Name.ToUpper().Contains(RemovLab_lot)).ToList();

                                        if (VeriLab_lot == null || VeriLab_lot.Count == 0)
                                        {
                                            DatosMalosPlant.Labor_lot = "La labor del lote: '" + r[15].ToString() + "' no se encontro.";
                                            r[15] = DatosMalosPlant.Labor_lot;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            if (VeriLab_lot.Count() > 1)
                                            {
                                                var VeriV_Lablot = VeriLab_lot.Where(x => x.Name == RemovLab_lot).ToList();

                                                if (VeriV_Lablot.Count() == 0)
                                                {
                                                    string TypeOwners = "";
                                                    string TypeOwnersE = ", ";

                                                    int countR = 1;

                                                    foreach (var item in VeriLab_lot)
                                                    {
                                                        if (countR == VeriV_Lablot.Count())
                                                        {
                                                            if (countR == 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Name + ".";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (countR == 1)
                                                            {
                                                                TypeOwners = item.Name;
                                                            }
                                                            else
                                                            {
                                                                if (countR <= 2)
                                                                {
                                                                    TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                                }
                                                                else
                                                                {
                                                                    TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                                }
                                                            }
                                                            countR++;
                                                        }
                                                    }

                                                    DatosMalosPlant.Labor_lot = "Se encontro multiples resultados para la labor del lote: '" + r[15].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                    r[15] = DatosMalosPlant.Labor_lot;
                                                    C_Bad++;
                                                }
                                                else
                                                {
                                                    string IdL_labor = VeriV_Lablot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                    r[15] = IdL_labor;
                                                    C_Good++;
                                                }
                                            }
                                            else if (VeriLab_lot.Count() == 1)
                                            {
                                                string IdL_labor = VeriLab_lot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                r[15] = IdL_labor;
                                                C_Good++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Labor_lot = "Campo Obligatorio";
                                    r[15] = DatosMalosPlant.Labor_lot;
                                    C_Bad++;
                                }

                                //Verificar tipo del lote
                                bool Tipo_lot = VerifyNull(r[16].ToString());
                                if (Tipo_lot == false)
                                {
                                    bool IsNumber = VerifyNumber(r[16].ToString());
                                    if (IsNumber == true)
                                    {
                                        DatosMalosPlant.Tip_lot = "Este campo no puede contener números.";
                                        r[16] = DatosMalosPlant.Tip_lot;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovTipo_lot = RemoveDiacritics(r[16].ToString().ToUpper());

                                        var VeriTipo_lot = List_TL.Where(x => x.Name.ToUpper().Contains(RemovTipo_lot)).ToList();

                                        if (VeriTipo_lot == null || VeriTipo_lot.Count == 0)
                                        {
                                            DatosMalosPlant.Tip_lot = "El tipo del lote: '" + r[16].ToString() + "' no se encontro.";
                                            r[16] = DatosMalosPlant.Tip_lot;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            if (VeriTipo_lot.Count() > 1)
                                            {
                                                var VeriV_Tipo_lot = VeriTipo_lot.Where(x => x.Name == RemovTipo_lot).ToList();

                                                if (VeriV_Tipo_lot.Count() == 0)
                                                {
                                                    string TypeOwners = "";
                                                    string TypeOwnersE = ", ";

                                                    int countR = 1;

                                                    foreach (var item in VeriTipo_lot)
                                                    {
                                                        if (countR == VeriV_Tipo_lot.Count())
                                                        {
                                                            if (countR == 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Name + ".";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (countR == 1)
                                                            {
                                                                TypeOwners = item.Name;
                                                            }
                                                            else
                                                            {
                                                                if (countR <= 2)
                                                                {
                                                                    TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                                }
                                                                else
                                                                {
                                                                    TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                                }
                                                            }
                                                            countR++;
                                                        }
                                                    }

                                                    DatosMalosPlant.Tip_lot = "Se encontro multiples resultados para el tipo del lote: '" + r[16].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                    r[16] = DatosMalosPlant.Tip_lot;
                                                    C_Bad++;
                                                }
                                                else
                                                {
                                                    string IdTip_lot = VeriV_Tipo_lot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                    r[16] = IdTip_lot;
                                                    C_Good++;
                                                }
                                            }
                                            else if (VeriTipo_lot.Count() == 1)
                                            {
                                                string IdTip_lot = VeriTipo_lot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                r[16] = IdTip_lot;
                                                C_Good++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Tip_lot = "Campo Obligatorio";
                                    r[16] = DatosMalosPlant.Tip_lot;
                                    C_Bad++;
                                }

                                //Verificar forma de siembra
                                bool FormS_lot = VerifyNull(r[17].ToString());
                                if (FormS_lot == false)
                                {
                                    bool IsNumber = VerifyNumber(r[17].ToString());
                                    if (IsNumber == true)
                                    {
                                        DatosMalosPlant.Form_lot = "Este campo no puede contener números.";
                                        r[17] = DatosMalosPlant.Form_lot;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovFormS_lot = RemoveDiacritics(r[17].ToString().ToUpper());

                                        var VeriFormS_lot = List_FS.Where(x => x.Name.ToUpper().Contains(RemovFormS_lot)).ToList();

                                        if (VeriFormS_lot == null || VeriFormS_lot.Count == 0)
                                        {
                                            DatosMalosPlant.Form_lot = "La forma de siembra del lote: '" + r[17].ToString() + "' no se encontro.";
                                            r[17] = DatosMalosPlant.Form_lot;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            if (VeriFormS_lot.Count() > 1)
                                            {
                                                var VeriV_FormS_lot = VeriFormS_lot.Where(x => x.Name == RemovFormS_lot).ToList();

                                                if (VeriV_FormS_lot.Count() == 0)
                                                {
                                                    string TypeOwners = "";
                                                    string TypeOwnersE = ", ";

                                                    int countR = 1;

                                                    foreach (var item in VeriFormS_lot)
                                                    {
                                                        if (countR == VeriV_FormS_lot.Count())
                                                        {
                                                            if (countR == 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Name + ".";
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Name + ".";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (countR == 1)
                                                            {
                                                                TypeOwners = item.Name;
                                                            }
                                                            else
                                                            {
                                                                if (countR <= 2)
                                                                {
                                                                    TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                                }
                                                                else
                                                                {
                                                                    TypeOwners = TypeOwners + item.Name + TypeOwnersE;
                                                                }
                                                            }
                                                            countR++;
                                                        }
                                                    }

                                                    DatosMalosPlant.Form_lot = "Se encontro multiples resultados para la forma de siembra del lote: '" + r[17].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                    r[17] = DatosMalosPlant.Form_lot;
                                                    C_Bad++;
                                                }
                                                else
                                                {
                                                    string IdForm_lot = VeriV_FormS_lot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                    r[17] = IdForm_lot;
                                                    C_Good++;
                                                }
                                            }
                                            else if (VeriFormS_lot.Count() == 1)
                                            {
                                                string IdForm_lot = VeriFormS_lot.Select(x => x.Name.ToString()).FirstOrDefault();
                                                r[17] = IdForm_lot;
                                                C_Good++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.Form_lot = "Campo Obligatorio";
                                    r[17] = DatosMalosPlant.Form_lot;
                                    C_Bad++;
                                }

                                //Verificar numero de ejes
                                bool FormNE_lot = VerifyNull(r[18].ToString());
                                if (FormS_lot == false)
                                {
                                    bool IsNumber = VerifyNumber(r[18].ToString());

                                    if (IsNumber == false)
                                    {
                                        DatosMalosPlant.NumEjes_lot = "Este campo no puede contener letras.";
                                        r[18] = DatosMalosPlant.NumEjes_lot;
                                        C_Bad++;
                                    }
                                    else {
                                        string RemovFormNE_lot = RemoveDiacritics(r[18].ToString().ToUpper());

                                        var VeriFormS_lot = List_NE.Where(x => x.Name.ToUpper() == RemovFormNE_lot).ToList();

                                        if (VeriFormS_lot == null || VeriFormS_lot.Count == 0)
                                        {
                                            DatosMalosPlant.NumEjes_lot = "El número de ejes: '" + r[18].ToString() + "' no se encontro.";
                                            r[18] = DatosMalosPlant.NumEjes_lot;
                                            C_Bad++;
                                        }
                                        else if (VeriFormS_lot.Count == 1)
                                        {
                                            r[18] = RemovFormNE_lot;
                                            C_Good++;
                                        }
                                    }
                                }
                                else
                                {
                                    DatosMalosPlant.NumEjes_lot = "Campo Obligatorio";
                                    r[18] = DatosMalosPlant.NumEjes_lot;
                                    C_Bad++;
                                }

                                //INSERTAR BUENAS A DATATABLE
                                count++;
                                if (C_Good == 17)
                                {
                                    DataRow drG = data_Good.NewRow();
                                    data_Good.ImportRow(r);
                                }
                                //INSERTAR MALAS A DATATABLE
                                else if (C_Bad > 0)
                                {
                                    DataRow drB = data_Bad.NewRow();
                                    data_Bad.ImportRow(r);
                                    DatosMalosPlant.Codigo_F = C_F3.Trim();
                                    FieldsPL.Add(DatosMalosPlant);
                                }

                                C_Good = 0;
                                C_Bad = 0;
                            }


                            if (data_Bad.Rows.Count > 0)
                            {
                                ViewBag.DataTableE = FieldsPL;
                                ViewBag.Status = 0;
                                ViewBag.Description = "Se encontro errores en el archivo, descargue el excel para ver los errores";
                                ViewBag.F_Goods = data_Good.Rows.Count;
                                ViewBag.F_Bad = data_Bad.Rows.Count;

                                Session["ErroresPlantations"] = FieldsPL;
                                //ToExcel(FieldsE);
                            }

                            if (data_Good.Rows.Count > 0)
                            {
                                List<Parametros> lstParametros = new List<Parametros>();
                                lstParametros.Add(new Parametros("@ResultadoOUTPUT", SqlDbType.NVarChar, 2000));
                                lstParametros.Add(new Parametros("@tablaTemp", data_Good));

                                var test = await _driver.EjecutarSp("Upload_DataPlantations", lstParametros);

                                if (test.Count == 0)
                                {
                                    string result = lstParametros[0].Valor.ToString();
                                    string[] respuesta = lstParametros[0].Valor.ToString().Split(',');

                                    ViewBag.Status = null;
                                    ViewBag.Description = null;

                                    if (respuesta[0] != null && respuesta[1] != null && respuesta[2] != null)
                                    {
                                        ViewBag.Status = respuesta[0];
                                        ViewBag.Description = respuesta[1];
                                        ViewBag.F_Goods = respuesta[2];
                                        ViewBag.F_Bad = data_Bad.Rows.Count;
                                    }

                                }
                                else
                                {
                                    ViewBag.Description = "Hubo un error en el procedimiento: " + test[0].Description;
                                    ViewBag.LineNumber = test[0].LineNumber;
                                    ViewBag.Procedure = test[0].Procedure;
                                    ViewBag.F_Goods = 0;
                                    ViewBag.F_Bad = data_Bad.Rows.Count;
                                }
                            }
                            return View("InterfacePlantations");
                        }
                    }

                    if (type == 2)
                    {
                        if (dato != 10 && ext == ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 10";
                            ListError.Add(er);
                            ViewBag.ListaE = ListError;
                            return View("InterfaceFamily", ViewBag.ListaE);
                        }
                        else if (dato != 10 && ext != ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 10";
                            ListError.Add(er);

                            er1.type = "Extension";
                            er1.Description = "El archivo que esta intentando subir tiene la extension " + ext + ", solo se puede subir archivos con extension .csv delimitado por ; (punto y coma)";
                            ListError.Add(er1);

                            ViewBag.ListaE = ListError;

                            return View("InterfaceFamily", ViewBag.ListaE);
                        }
                        else
                        {
                            List<Family_Error> FieldsFME = new List<Family_Error>();
                            List<CedulaList> ListCedula = new List<CedulaList>();

                            int count = 2;
                            int C_Bad = 0;
                            int C_Good = 0;


                            DataTable data_Good = dtTemporal.Clone();
                            DataTable data_Bad = dtTemporal.Clone();


                            //foreach (DataRow r in dtTemporal.Rows)
                            //{
                            //    r[4].ToString()
                            //}


                            foreach (DataRow r in dtTemporal.Rows)
                            {
                                Family_Error DatosMalosFM = new Family_Error();
                                CedulaList Cedu = new CedulaList();
                                DatosMalosFM.Fila = count;

                                string C_F = r[0].ToString();

                                bool C_finca = VerifyNull(r[0].ToString());
                                if (C_finca == true)
                                {
                                    DatosMalosFM.Codigo_F = "Campo Obligatorio";
                                    r[0] = DatosMalosFM.Codigo_F;
                                    C_Bad++;
                                }
                                else
                                {
                                    //validar codigo de finca
                                    bool IsNumber = VerifyNumber(r[0].ToString());
                                    if (IsNumber == true)
                                    {
                                        var CodeV = r[0].ToString().Trim();
                                        var VeriCode = db.Farms.Where(x => x.Code == CodeV).ToList();

                                        if (VeriCode.Count() > 0)
                                        {
                                            if (VeriCode.Count() > 1)
                                            {
                                                DatosMalosFM.Codigo_F = "El codigo: " + CodeV + " se encontro mas de una vez en la base de datos.";
                                                r[0] = DatosMalosFM.Codigo_F;
                                                C_Bad++;
                                            }
                                            else if (VeriCode.Count() == 1)
                                            {
                                                foreach (var item in VeriCode)
                                                {
                                                    r[0] = item.Id.ToString();
                                                    C_Good++;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            DatosMalosFM.Codigo_F = "El codigo: " + CodeV + " no se encontro en la base de datos.";
                                            r[0] = DatosMalosFM.Codigo_F;
                                            C_Bad++;
                                        }
                                    }
                                    else
                                    {
                                        DatosMalosFM.Codigo_F = "Este campo no puede contener letras";
                                        r[0] = DatosMalosFM.Codigo_F;
                                        C_Bad++;
                                    }

                                }

                                //validar nombre
                                bool Nombre = VerifyNull(r[1].ToString());
                                if (Nombre == true)
                                {
                                    DatosMalosFM.Nombres = "Campo Obligatorio";
                                    r[1] = DatosMalosFM.Nombres;
                                    C_Bad++;
                                }
                                else
                                {
                                    bool IsLetters = VerifyLetters(r[1].ToString());

                                    if (IsLetters == true)
                                    {
                                        r[1] = r[1].ToString().TrimStart().TrimEnd();
                                        C_Good++;
                                    }
                                    else
                                    {
                                        DatosMalosFM.Nombres = "Este campo no puede contener numeros: " + r[1].ToString().TrimStart().TrimEnd();
                                        r[1] = DatosMalosFM.Nombres;
                                        C_Bad++;
                                    }

                                }

                                //validar apellidos
                                bool Apellidos = VerifyNull(r[2].ToString());
                                if (Apellidos == true)
                                {
                                    //DatosMalosFM.Apellidos = "Campo Obligatorio";
                                    //r[2] = DatosMalosFM.Apellidos;
                                    //C_Bad++;

                                    r[2] = "";
                                    C_Good++;
                                }
                                else
                                {
                                    bool IsLetters = VerifyLetters(r[2].ToString());

                                    if (IsLetters == true)
                                    {
                                        r[2] = r[2].ToString().TrimStart().TrimEnd();
                                        C_Good++;
                                    }
                                    else
                                    {
                                        DatosMalosFM.Apellidos = "Este campo no puede contener numeros: " + r[2].ToString().TrimStart().TrimEnd();
                                        r[2] = DatosMalosFM.Apellidos;
                                        C_Bad++;
                                    }

                                }

                                //validar fecha
                                bool F_Naci = VerifyNull(r[3].ToString());
                                if (F_Naci == true)
                                {
                                    //DatosMalosFM.F_naci = "Campo Obligatorio";
                                    r[3] = "1900-01-01";
                                    C_Good++;
                                }
                                else
                                {
                                    var date = r[3].ToString().TrimEnd().TrimStart();
                                    DateTime dateValue;

                                    if (DateTime.TryParseExact(date, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                                    {
                                        r[3] = r[3].ToString().TrimEnd().TrimStart().Replace("-", "");
                                        C_Good++;
                                    }
                                    else
                                    {
                                        DatosMalosFM.F_naci = "El formato de la fecha: " + date + " debe ser del tipo 'yyyy-MM-dd'.";
                                        r[3] = DatosMalosFM.F_naci;
                                        C_Bad++;
                                    }
                                }

                                //validar cedula
                                bool Cedula = VerifyNull(r[4].ToString());
                                var CC_Error = r[4].ToString();
                                if (Cedula == true)
                                {
                                    DatosMalosFM.Cedula = "Campo Obligatorio";
                                    r[4] = DatosMalosFM.Cedula;
                                    C_Bad++;
                                }
                                else
                                {
                                    Guid guidOutput = Guid.Empty;
                                    bool isValid = Guid.TryParse(r[0].ToString(), out guidOutput);

                                    //verifico que la cedula no exista 
                                    var exit = ListCedula.Where(x => x.Cedula == r[4].ToString()).FirstOrDefault();

                                    if (exit != null)
                                    {
                                        Cedu.Cedula = r[4].ToString();
                                        ListCedula.Add(Cedu);

                                        DatosMalosFM.Cedula = "La cedula '" + r[4].ToString() + "' se encontro mas de una vez en el listado.";
                                        r[4] = DatosMalosFM.Cedula;
                                        C_Bad++;
                                    }
                                    else
                                    {

                                        Cedu.Cedula = r[4].ToString();
                                        ListCedula.Add(Cedu);

                                        if (isValid == false)
                                        {
                                            DatosMalosFM.Cedula = "No se pude verificar la cedula: " + r[4].ToString() + " por que no se encontro el codigo de la finca: " + C_F;
                                            r[4] = DatosMalosFM.Cedula;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            C_Good++;
                                        }
                                    }
                                }

                                //validar educacion
                                bool educacion = VerifyNull(r[5].ToString());
                                if (educacion == true)
                                {
                                    //DatosMalosFM.Educacion = "Campo Obligatorio";
                                    //r[5] = DatosMalosFM.Educacion;
                                    //C_Bad++;


                                    r[5] = "Ninguno";
                                    C_Good++;
                                }
                                else
                                {
                                    string RemovEducation = RemoveDiacritics(r[5].ToString().ToUpper());

                                    // List with data to syncronize with mobile app
                                    var list = FamilyUnitMember.InitializeList();
                                    var Edu = list[FamilyUnitMember.EDUCATION_LIST];

                                    List<Listas> datos = new List<Listas>();
                                    foreach (var item in Edu)
                                    {
                                        Listas dn = new Listas();

                                        dn.Name = RemoveDiacritics(item.ToUpper());
                                        dn.Origin = item;
                                        datos.Add(dn);
                                    }

                                    var ExistEedu = datos.Where(x => x.Name.Contains(RemovEducation)).ToList();

                                    if (ExistEedu.Count() == 0)
                                    {
                                        DatosMalosFM.Educacion = "No se encontro el tipo de educación: '" + r[5].ToString() + "'";
                                        r[5] = DatosMalosFM.Educacion;
                                        C_Bad++;
                                    }
                                    else
                                    {

                                        if (ExistEedu.Count() > 1)
                                        {
                                            var ExistEedu2 = datos.Where(x => x.Name == RemovEducation).ToList();

                                            if (ExistEedu2.Count() == 0)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countO = 1;

                                                foreach (var item in ExistEedu)
                                                {
                                                    if (countO == ExistEedu.Count())
                                                    {
                                                        if (countO == 2)
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Origin + ".";
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + item.Origin + ".";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (countO == 1)
                                                        {
                                                            TypeOwners = item.Origin;
                                                        }
                                                        else
                                                        {
                                                            if (countO <= 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Origin + TypeOwnersE;
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Origin + TypeOwnersE;
                                                            }
                                                        }
                                                        countO++;
                                                    }
                                                }

                                                DatosMalosFM.Educacion = "Se encontro multiples resultados para  el tipo de educacion: '" + r[5].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[5] = DatosMalosFM.Educacion;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[5] = ExistEedu2.Select(x => x.Origin).FirstOrDefault();
                                                C_Good++;
                                            }

                                        }
                                        else
                                        {
                                            r[5] = ExistEedu.Select(x => x.Origin).FirstOrDefault();
                                            C_Good++;
                                        }
                                    }
                                }

                                //Verificar relacion
                                bool relacion = VerifyNull(r[6].ToString());
                                if (relacion == true)
                                {
                                    //DatosMalosFM.Relacion = "Campo Obligatorio";
                                    //r[6] = DatosMalosFM.Relacion;
                                    //C_Bad++;

                                    r[6] = "Ninguno";
                                    C_Good++;
                                }
                                else
                                {
                                    string RemovRelation = RemoveDiacritics(r[6].ToString().ToUpper());

                                    // List with data to syncronize with mobile app
                                    var list = FamilyUnitMember.InitializeList();
                                    var Rela = list[FamilyUnitMember.RELATIONSHIP_LIST];

                                    List<Listas> datosR = new List<Listas>();
                                    foreach (var item in Rela)
                                    {
                                        Listas dnR = new Listas();

                                        dnR.Name = RemoveDiacritics(item.ToUpper());
                                        dnR.Origin = item;
                                        datosR.Add(dnR);
                                    }

                                    var ExistRela = datosR.Where(x => x.Name.Contains(RemovRelation)).ToList();

                                    if (ExistRela.Count() == 0)
                                    {
                                        DatosMalosFM.Relacion = "No se encontro el tipo de relación: '" + r[6].ToString() + "'";
                                        r[6] = DatosMalosFM.Relacion;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (ExistRela.Count() > 1)
                                        {
                                            var ExistRela2 = datosR.Where(x => x.Name == RemovRelation).ToList();

                                            if (ExistRela2.Count() == 0)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countR = 1;

                                                foreach (var item in ExistRela)
                                                {
                                                    if (countR == ExistRela.Count())
                                                    {
                                                        if (countR == 2)
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Origin + ".";
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + item.Origin + ".";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (countR == 1)
                                                        {
                                                            TypeOwners = item.Origin;
                                                        }
                                                        else
                                                        {
                                                            if (countR <= 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Origin + TypeOwnersE;
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Origin + TypeOwnersE;
                                                            }
                                                        }
                                                        countR++;
                                                    }
                                                }

                                                DatosMalosFM.Relacion = "Se encontro multiples resultados para  el tipo de relación: '" + r[6].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[6] = DatosMalosFM.Relacion;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[6] = ExistRela2.Select(x => x.Origin).FirstOrDefault();
                                                C_Good++;
                                            }

                                        }
                                        else
                                        {
                                            r[6] = ExistRela.Select(x => x.Origin).FirstOrDefault();
                                            C_Good++;
                                        }
                                    }
                                }

                                //Verificar Estado civil
                                bool Est_Civil = VerifyNull(r[7].ToString());
                                if (Est_Civil == true)
                                {
                                    //DatosMalosFM.E_Civil = "Campo Obligatorio";
                                    //r[7] = DatosMalosFM.E_Civil;
                                    //C_Bad++;


                                    r[7] = "Ninguno";
                                    C_Good++;
                                }
                                else
                                {
                                    string RemovEst_Civil = RemoveDiacritics(r[7].ToString().ToUpper());

                                    // List with data to syncronize with mobile app
                                    var list = FamilyUnitMember.InitializeList();
                                    var Es_C = list[FamilyUnitMember.MARITAL_STATUS_LIST];

                                    List<Listas> datosEC = new List<Listas>();
                                    foreach (var item in Es_C)
                                    {
                                        Listas dnEC = new Listas();

                                        dnEC.Name = RemoveDiacritics(item.ToUpper());
                                        dnEC.Origin = item;
                                        datosEC.Add(dnEC);
                                    }

                                    var ExistEC = datosEC.Where(x => x.Name.Contains(RemovEst_Civil)).ToList();

                                    if (ExistEC.Count() == 0)
                                    {
                                        DatosMalosFM.E_Civil = "No se encontro el tipo de estado civil: '" + r[7].ToString() + "'";
                                        r[7] = DatosMalosFM.E_Civil;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (ExistEC.Count() > 1)
                                        {
                                            var ExistEC2 = datosEC.Where(x => x.Name == RemovEst_Civil).ToList();

                                            if (ExistEC2.Count() == 0)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countEC = 1;

                                                foreach (var item in ExistEC)
                                                {
                                                    if (countEC == ExistEC.Count())
                                                    {
                                                        if (countEC == 2)
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Origin + ".";
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + item.Origin + ".";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (countEC == 1)
                                                        {
                                                            TypeOwners = item.Origin;
                                                        }
                                                        else
                                                        {
                                                            if (countEC <= 2)
                                                            {
                                                                TypeOwners = TypeOwners + TypeOwnersE + item.Origin + TypeOwnersE;
                                                            }
                                                            else
                                                            {
                                                                TypeOwners = TypeOwners + item.Origin + TypeOwnersE;
                                                            }
                                                        }
                                                        countEC++;
                                                    }
                                                }

                                                DatosMalosFM.E_Civil = "Se encontro multiples resultados para  el tipo de estado civil: '" + r[7].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[7] = DatosMalosFM.E_Civil;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[7] = ExistEC2.Select(x => x.Origin).FirstOrDefault();
                                                C_Good++;
                                            }

                                        }
                                        else
                                        {
                                            r[7] = ExistEC.Select(x => x.Origin).FirstOrDefault();
                                            C_Good++;
                                        }
                                    }
                                }

                                //Validar propietario
                                bool Prop = VerifyNull(r[8].ToString());
                                if (Prop == true)
                                {
                                    DatosMalosFM.Propietario = "Campo Obligatorio";
                                    r[8] = DatosMalosFM.Propietario;
                                    C_Bad++;
                                }
                                else
                                {
                                    string prop = RemoveDiacritics(r[8].ToString().ToUpper());

                                    bool Yes = prop.Contains("SI");
                                    bool Not = prop.Contains("NO");


                                    if ((Yes == false && Not == false) || (Yes == true && Not == true))
                                    {
                                        DatosMalosFM.Propietario = "El dato: '" + r[8].ToString() + "' no es aceptado, solo puede colocar SI o NO.";
                                        r[8] = DatosMalosFM.Propietario;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        Guid guidOutput = Guid.Empty;
                                        bool isValid = Guid.TryParse(r[0].ToString(), out guidOutput);

                                        if (Yes == true)
                                        {
                                            if (isValid == false)
                                            {
                                                DatosMalosFM.Propietario = "El codigo de finca: '" + C_F + "' no se encontro en la base de datos, por lo tanto no se pudo verificar el propietario";
                                                r[8] = DatosMalosFM.Propietario;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                var VerifyOwn = db.FamilyUnitMembers.Where(x => x.FarmId == guidOutput && x.IsOwner == true).ToList();

                                                if (VerifyOwn.Count() == 0)
                                                {
                                                    r[8] = 1;
                                                    C_Good++;
                                                }
                                                else if (VerifyOwn.Count() > 0)
                                                {
                                                    var farm = _manager.Details(new Guid(r[0].ToString()));
                                                    foreach (var member in farm.FamilyUnitMembers)
                                                    {
                                                        member.IsOwner = false;
                                                    }
                                                    _manager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);

                                                    r[8] = 1;
                                                    C_Good++;

                                                }
                                            }
                                        }

                                        if (Not == true)
                                        {
                                            if (isValid == false)
                                            {
                                                DatosMalosFM.Propietario = "El codigo de finca: '" + C_F + "' no se encontro en la base de datos, por lo tanto no se pudo verificar el propietario";
                                                r[8] = DatosMalosFM.Propietario;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[8] = 0;
                                                C_Good++;
                                            }
                                        }
                                    }
                                }

                                //Validar Telefono
                                bool Tel = VerifyNull(r[9].ToString());
                                if (Tel == false)
                                {
                                    bool VerifyTel = VerifyLetters(r[9].ToString());
                                    if (VerifyTel == true)
                                    {
                                        DatosMalosFM.Telefono = "El telefono: '" + r[9].ToString() + "' no contener letras, ni caracteres especiales.";
                                        r[9] = DatosMalosFM.Telefono;
                                        C_Bad++;
                                    }
                                }

                                count++;

                                //INSERTAR BUENAS A DATATABLE
                                if (C_Good == 9)
                                {
                                    DataRow drG = data_Good.NewRow();
                                    data_Good.ImportRow(r);
                                }
                                //INSERTAR MALAS A DATATABLE
                                else if (C_Bad > 0)
                                {
                                    DataRow drB = data_Bad.NewRow();
                                    data_Bad.ImportRow(r);
                                    DatosMalosFM.Codigo_F = C_F.Trim();
                                    if (DatosMalosFM.Cedula == null || DatosMalosFM.Cedula == "")
                                    {
                                        DatosMalosFM.Cedula = CC_Error;
                                    }
                                    FieldsFME.Add(DatosMalosFM);
                                }

                                C_Good = 0;
                                C_Bad = 0;

                            }

                            if (data_Bad.Rows.Count > 0)
                            {
                                ViewBag.DataTableE = FieldsFME;
                                ViewBag.F_Bad = data_Bad.Rows.Count;
                                ViewBag.Description = "Se encontraron errores en el excel.";

                                Session["ErroresFamily"] = FieldsFME;
                                //ToExcel(FieldsE);
                            }

                            if (data_Good.Rows.Count > 0)
                            {
                                List<Parametros> lstParametros = new List<Parametros>();
                                lstParametros.Add(new Parametros("@ResultadoOUTPUT", SqlDbType.NVarChar, 2000));
                                lstParametros.Add(new Parametros("@tablaTemp", data_Good));

                                var test = await _driver.EjecutarSp("Upload_DataFamily", lstParametros);
                                //var test = await _driver.EjecutarSp("Upload_DataFamilyPruebas", lstParametros);


                                if (test.Count == 0)
                                {
                                    string result = lstParametros[0].Valor.ToString();
                                    string[] respuesta = lstParametros[0].Valor.ToString().Split(',');

                                    ViewBag.Status = null;
                                    ViewBag.Description = null;


                                    if (respuesta[0] != null && respuesta[1] != null && respuesta[2] != null)
                                    {
                                        ViewBag.Status = respuesta[0];
                                        ViewBag.Description = respuesta[1];
                                        ViewBag.F_Goods = respuesta[2];
                                        ViewBag.F_Bad = data_Bad.Rows.Count;
                                    }

                                }
                                else
                                {
                                    ViewBag.Description = "Hubo un error en el procedimiento: " + test[0].Description;
                                    ViewBag.LineNumber = test[0].LineNumber;
                                    ViewBag.Procedure = test[0].Procedure;
                                    ViewBag.F_Goods = 0;
                                    ViewBag.F_Bad = data_Bad.Rows.Count;
                                }
                            }
                            return View("InterfaceFamily");
                        }
                    }

                    if (type == 1)
                    {

                        if (dato != 10 && ext == ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 10";
                            ListError.Add(er);
                            ViewBag.ListaE = ListError;
                            return View("InterfaceFarms", ViewBag.ListaE);
                        }
                        else if (dato != 10 && ext != ".csv")
                        {
                            er.type = "Columnas";
                            er.Description = "Esta intentando cargar " + dato + " columnas donde corresponden 10";
                            ListError.Add(er);

                            er1.type = "Extension";
                            er1.Description = "El archivo que esta intentando subir tiene la extension " + ext + ", solo se puede subir archivos con extension .csv delimitado por ; (punto y coma)";
                            ListError.Add(er1);

                            ViewBag.ListaE = ListError;

                            return View("InterfaceFarms", ViewBag.ListaE);
                        }
                        else
                        {
                            List<F_Error> FieldsE = new List<F_Error>();

                            int count = 2;
                            int C_Bad = 0;
                            int C_Good = 0;


                            DataTable data_Good = dtTemporal.Clone();
                            DataTable data_Bad = dtTemporal.Clone();

                            foreach (DataRow r in dtTemporal.Rows)
                            {
                                F_Error DatosMalos = new F_Error();
                                DatosMalos.Fila = count;

                                //Verificar Campos obligatorios

                                //VERIFICAR NOMBRE
                                bool Nombre = VerifyNull(r[0].ToString());
                                if (Nombre == true)
                                {
                                    DatosMalos.Name = "Campo Obligatorio";
                                    r[0] = "Campo Obligatorio";
                                    C_Bad++;
                                }
                                else
                                {
                                    C_Good++;
                                }

                                //VERIFICAR CODIGO
                                string C_F2 = r[1].ToString();
                                bool LettersCod = VerifyLetters(r[1].ToString());

                                if (LettersCod == true)
                                {
                                    DatosMalos.Code = "El campo de codigo no puede contener letras, revise que no tenga trocados los datos de nombre y codigo.";
                                    r[1] = DatosMalos.Code;
                                    C_F2 = DatosMalos.Code;
                                    C_Bad++;
                                }
                                else
                                {
                                    bool Code = VerifyNull(r[1].ToString());
                                    if (Code == true)
                                    {
                                        DatosMalos.Code = "Campo Obligatorio";
                                        r[1] = "Campo Obligatorio";
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        var CodeV = r[1].ToString().Trim();

                                        var VeriCode = db.Farms.Where(x => x.Code == CodeV).ToList();

                                        if (VeriCode.Count <= 1 && LettersCod == false)
                                        {
                                            r[1] = CodeV;
                                            C_Good++;
                                        }
                                        else
                                        {
                                            DatosMalos.Code = "El codigo: " + CodeV + " se encontro mas de una vez en la base de datos, verifique que este registro no este asociado a un productor que tiene mas de una finca, si es asi por favor anexe el codigo seguido de un guion mas el consecutivo de la siguiente finca ej:finca 1= '1053859655', finca 2= '1053859655-2'.";
                                            r[1] = DatosMalos.Code;
                                            C_Bad++;
                                        }
                                    }
                                }

                                //VERIFICAR GEOREFERENCIACION
                                bool NullLat = VerifyNull(r[2].ToString());
                                bool NullLong = VerifyNull(r[3].ToString());

                                //var extraerlat = r[2].ToString().Substring(0, 1);
                                var extraerlong = r[3].ToString().Substring(0, 1);

                                //verificar si tiene letras
                                bool LettersLat = VerifyLetters(r[2].ToString());
                                bool LettersLong = VerifyLetters(r[3].ToString());

                                //se valida que la latitud y la longitud no esten nulos
                                if (NullLat == false && NullLong == false && extraerlong != "0")
                                {
                                    // se valida que la latitud no contenga letras
                                    // se valida que la longitud no contenga letras
                                    // se valida que la longitud empiece por -
                                    // se valida que la latitud sea un numero positivo
                                    if (LettersLat == false && LettersLong == false)
                                    {
                                        bool geoLatitud = false;
                                        bool geoLongitud = false;

                                        var cantPLat = r[2].ToString().Replace(".", ",").ToArray();
                                        var cantPLong = r[3].ToString().Replace(".", ",").ToArray();
                                        int cpLat = 0; //cantidad puntos en latitud
                                        int cpLon = 0; //cantidad puntos en longitud

                                        //foreach para validar la cantidad de puntos en latitud
                                        foreach (var item in cantPLat)
                                        {
                                            if (item.ToString().Contains(","))
                                            {
                                                cpLat++;
                                            }
                                        }

                                        //foreach para validar la cantidad de puntos en longitud
                                        foreach (var item in cantPLong)
                                        {
                                            if (item.ToString().Contains(","))
                                            {
                                                cpLon++;
                                            }
                                        }

                                        double lat = 0;
                                        double lon = 0;

                                        //si cantidad de puntos en latitud es mayor a 1 entra por aca
                                        if (cpLat > 1)
                                        {
                                            DatosMalos.Lat = "La latitud tiene un formato incorrecto, solo puede tener un punto (.)";
                                            r[2] = DatosMalos.Lat;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            lat = Convert.ToDouble(r[2].ToString().Replace(".", ","));
                                        }

                                        //si cantidad de puntos en longitud es mayor a 1 entra por aca
                                        if (cpLat > 1)
                                        {
                                            DatosMalos.Long = "La longitud tiene un formato incorrecto, solo puede tener un punto (.)";
                                            r[3] = DatosMalos.Long;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            lon = Convert.ToDouble(r[3].ToString().Replace(".", ","));
                                        }

                                        //si la cantidad de puntos en latitud y longitud es igual a 1 
                                        if (cpLat == 1 && cpLat == 1)
                                        {
                                            //valida si la latitud esta comprendida entre -4 y 12
                                            if (lat >= -4 && lat <= 12)
                                            {
                                                geoLatitud = true;
                                            }

                                            //valida si la longitud esta comprendida entre -60 y -80
                                            if (lon >= -80 && lon <= -60)
                                            {
                                                geoLongitud = true;
                                            }

                                            //mensaje para cuando la latitud esta por fuera de los limites de colombia
                                            if (geoLatitud == false)
                                            {
                                                DatosMalos.Lat = "La latitud " + lat + " esta por fuera de los limites de Colombia (-4 a 12)";
                                                r[2] = DatosMalos.Lat;
                                                C_Bad++;
                                            }
                                            //else
                                            //{
                                            //    C_Good++;
                                            //}

                                            //mensaje para cuando la longitud esta por fuera de los limites de colombia
                                            if (geoLongitud == false)
                                            {
                                                DatosMalos.Long = "La longitud " + lon + " esta por fuera de los limites de Colombia (-60 a -80) ";
                                                r[3] = DatosMalos.Long;
                                                C_Bad++;
                                            }
                                            //else
                                            //{
                                            //    C_Good++;
                                            //}
                                        }
                                    }
                                    //si latitud contiene letras pero la longitud no
                                    else if (LettersLat == true && LettersLong == false)
                                    {
                                        DatosMalos.Lat = "Formato incorrecto, la latitud " + r[2].ToString() + " no puede contener letras";
                                        r[2] = DatosMalos.Lat;
                                        C_Bad++;

                                    }
                                    //si latitud no contiene letras pero la longitud si
                                    else if (LettersLat == false && LettersLong == true)
                                    {
                                        DatosMalos.Long = "Formato incorrecto, la longitud " + r[3].ToString() + " no puede contener letras";
                                        r[3] = DatosMalos.Long;
                                        C_Bad++;
                                    }
                                    //si la latitud y la longitud tienen letras
                                    else if (LettersLat == true && LettersLong == true)
                                    {
                                        DatosMalos.Lat = "Formato incorrecto, la latitud " + r[2].ToString() + " no puede contener letras";
                                        r[2] = DatosMalos.Lat;
                                        C_Bad++;

                                        DatosMalos.Long = "Formato incorrecto, la longitud " + r[3].ToString() + " no puede contener letras";
                                        r[3] = DatosMalos.Long;
                                        C_Bad++;
                                    }
                                }
                                //si la latitud esta vacia pero la longitud no 
                                else if (NullLat == true && NullLong == false)
                                {
                                    DatosMalos.Lat = "La latitud se encuentra vacia pero la longitud no, si va a registrar georeferenciacion es necesario ingresar los dos datos";
                                    r[2] = DatosMalos.Lat;
                                    C_Bad++;
                                }
                                //si la longitud esta vacia pero la latitud no
                                else if (NullLat == false && NullLong == true)
                                {
                                    DatosMalos.Long = "La longitud se encuentra vacia pero la latitud no, si va a registrar georeferenciacion es necesario ingresar los dos datos";
                                    r[3] = DatosMalos.Long;
                                    C_Bad++;
                                }
                                //si la latitud y la longitud estan vacias
                                else if (NullLat == true && NullLong == true)
                                {
                                    //DatosMalos.Lat = "La latitud se encuentra vacia, si va a registrar georeferenciacion es necesario ingresar este dato";
                                    //r[2] = DatosMalos.Lat;
                                    //C_Bad++;

                                    //DatosMalos.Long = "La longitud se encuentra vacia, si va a registrar georeferenciacion es necesario ingresar este dato";
                                    //r[3] = DatosMalos.Long;
                                    //C_Bad++;

                                    r[2] = 0.00000000;
                                    r[3] = 0.00000000;
                                }
                                else if (extraerlong == "0")
                                {
                                    r[2] = 0.00000000;
                                    r[3] = 0.00000000;
                                }



                                //VERIFICAR MUNICIPIO Y VEREDA
                                bool Muni = VerifyNull(r[4].ToString());
                                bool Vill = VerifyNull(r[5].ToString());
                                if (Muni == true && Vill == true)
                                {
                                    DatosMalos.MunicipalityId = "Campo Obligatorio";
                                    r[4] = DatosMalos.MunicipalityId;

                                    DatosMalos.VillageId = "Campo Obligatorio";
                                    r[5] = DatosMalos.VillageId;

                                    C_Bad++;
                                }
                                else if (Muni == false && Vill == true)
                                {
                                    DatosMalos.VillageId = "Campo Obligatorio";
                                    r[5] = DatosMalos.VillageId;
                                    C_Bad++;
                                }
                                else if (Muni == true && Vill == false)
                                {
                                    DatosMalos.MunicipalityId = "Campo Obligatorio";
                                    r[4] = DatosMalos.MunicipalityId;
                                    C_Bad++;
                                }
                                else
                                {
                                    string RemovMuniT = RemoveDiacritics(r[4].ToString().ToUpper());

                                    var ExistMuni = Formated.Where(x => x.Name.Contains(RemovMuniT)).FirstOrDefault();

                                    if (ExistMuni == null)
                                    {
                                        DatosMalos.MunicipalityId = "El municipio " + r[4].ToString() + " no se encontro en la base de datos";
                                        r[4] = DatosMalos.MunicipalityId;

                                        DatosMalos.VillageId = "No se puede verificar la vereda por que el municipio no se encontro";
                                        r[5] = DatosMalos.VillageId;

                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovVillT = RemoveDiacritics(r[5].ToString().ToUpper());

                                        var FormatedVV = FormatedV.Where(x => x.Name.Contains(RemovVillT)).ToList();

                                        if (FormatedVV == null)
                                        {
                                            DatosMalos.VillageId = "La vereda + " + r[5].ToString() + "+ no se encontro en la base de datos";
                                            r[5] = DatosMalos.VillageId;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            var match = FormatedVV.Where(x => x.Muni == ExistMuni.Id).Select(x => x.Id).FirstOrDefault();

                                            if (match == null || match == new Guid("00000000-0000-0000-0000-000000000000"))
                                            {
                                                DatosMalos.VillageId = "La vereda: " + r[5].ToString() + " no esta asociada al municipio: " + r[4].ToString();
                                                r[5] = DatosMalos.VillageId;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                r[5] = match.ToString();
                                                C_Good++;
                                            }
                                        }
                                    }
                                }

                                //VERIFICAR TIPO DE PROPIETARIO
                                bool Owner = VerifyNull(r[6].ToString());
                                if (Owner == false)
                                {
                                    string RemovOwnT = RemoveDiacritics(r[6].ToString().ToUpper());
                                    var VeriOwn = db.OwnershipTypes.Where(x => x.Name.Contains(RemovOwnT) && x.DeletedAt == null).ToList();

                                    if (VeriOwn == null || VeriOwn.Count == 0)
                                    {
                                        DatosMalos.OwnershipTypeId = "El tipo de propietario: '" + r[6].ToString() + "' no se encontro en la base de datos";
                                        r[6] = DatosMalos.OwnershipTypeId;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (VeriOwn.Count() > 1)
                                        {
                                            string TypeOwners = "";
                                            string TypeOwnersE = ", ";

                                            int countO = 1;

                                            foreach (var item in VeriOwn)
                                            {
                                                if (countO == VeriOwn.Count())
                                                {
                                                    TypeOwners = TypeOwners + item.Name + ".";
                                                }
                                                else
                                                {
                                                    if (countO == 1)
                                                    {
                                                        TypeOwners = item.Name;
                                                    }
                                                    else
                                                    {
                                                        TypeOwners = TypeOwners + TypeOwnersE + item.Name;
                                                    }
                                                    countO++;
                                                }
                                            }
                                            DatosMalos.OwnershipTypeId = "Se encontro multiples resultados para la variable: '" + r[6].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                            r[6] = DatosMalos.OwnershipTypeId;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            string IdOwn = VeriOwn.Select(x => x.Id.ToString()).FirstOrDefault();
                                            r[6] = IdOwn;
                                        }

                                    }
                                }

                                //VERIFICAR ADMINISTRADOR ORGANIZACION
                                bool Org = VerifyNull(r[7].ToString());
                                var OrgP = r[7].ToString().ToUpper();
                                if (Org == true)
                                {
                                    DatosMalos.Organizacion = "Campo Obligatorio";
                                    r[7] = DatosMalos.Organizacion;
                                    C_Bad++;
                                }
                                else
                                {
                                    string RemovOrg = RemoveDiacritics(r[7].ToString().ToUpper());
                                    var veriOrg = db.SupplyChains.Where(x => x.Supplier.Name == RemovOrg && x.DeletedAt == null).ToList();

                                    if (veriOrg == null || veriOrg.Count() == 0)
                                    {
                                        DatosMalos.Organizacion = "No se encontro la organización '" + r[7].ToString() + "'";
                                        r[7] = DatosMalos.Organizacion;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string IdOrg = veriOrg.Select(x => x.SupplierId.ToString()).FirstOrDefault();
                                        r[7] = IdOrg;
                                        C_Good++;
                                    }
                                }

                                //VERIFICAR ADMINISTRADOR PROGRAMA
                                bool Admin = VerifyNull(r[8].ToString());
                                if (Admin == true)
                                {
                                    DatosMalos.SupplyChainId = "Campo Obligatorio";
                                    r[8] = DatosMalos.SupplyChainId;
                                    C_Bad++;
                                }
                                else
                                {
                                    Guid guidOutput = Guid.Empty;
                                    bool isValid = Guid.TryParse(r[7].ToString(), out guidOutput);

                                    if (isValid == false)
                                    {
                                        DatosMalos.SupplyChainId = "No se pude verificar el programa: " + r[8].ToString() + " por que no se encontro el codigo de la organización: " + OrgP;
                                        r[4] = DatosMalos.SupplyChainId;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        string RemovAdmin = RemoveDiacritics(r[8].ToString().ToUpper());
                                        var veriAdmin = db.SupplyChains.Where(x => x.Name == RemovAdmin && x.DeletedAt == null && x.SupplierId == guidOutput).ToList();

                                        if (veriAdmin == null)
                                        {
                                            DatosMalos.SupplyChainId = "No se encontro el administrador '" + r[8].ToString() + "'";
                                            r[7] = DatosMalos.SupplyChainId;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            if (veriAdmin.Count() > 1)
                                            {
                                                string TypeOwners = "";
                                                string TypeOwnersE = ", ";

                                                int countO = 1;

                                                foreach (var item in veriAdmin)
                                                {
                                                    if (countO == veriAdmin.Count())
                                                    {
                                                        TypeOwners = TypeOwners + item.Name + ".";
                                                    }
                                                    else
                                                    {
                                                        if (countO == 1)
                                                        {
                                                            TypeOwners = item.Name;
                                                        }
                                                        else
                                                        {
                                                            TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                        }
                                                        countO++;
                                                    }
                                                }
                                                DatosMalos.SupplyChainId = "Se encontro multiples resultados para la variable: '" + r[8].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                                r[7] = DatosMalos.SupplyChainId;
                                                C_Bad++;
                                            }
                                            else
                                            {
                                                string IdAdmin = veriAdmin.Select(x => x.Id.ToString()).FirstOrDefault();
                                                r[8] = IdAdmin;
                                                C_Good++;
                                            }
                                        }
                                    }
                                }

                                //Verificar Estado
                                bool State = VerifyNull(r[9].ToString());
                                if (State == true)
                                {
                                    DatosMalos.FarmStatusId = "Campo Obligatorio";
                                    r[9] = DatosMalos.FarmStatusId;
                                    C_Bad++;
                                }
                                else
                                {
                                    string RemovState = RemoveDiacritics(r[9].ToString().ToUpper());
                                    var veriState = db.FarmStatus.Where(x => x.Name.Contains(RemovState) && x.DeletedAt == null).ToList();

                                    if (veriState == null)
                                    {
                                        DatosMalos.FarmStatusId = "No se encontro el estado '" + r[9].ToString() + "'";
                                        r[9] = DatosMalos.FarmStatusId;
                                        C_Bad++;
                                    }
                                    else
                                    {
                                        if (veriState.Count() > 1)
                                        {
                                            string TypeOwners = "";
                                            string TypeOwnersE = ", ";

                                            int countO = 1;

                                            foreach (var item in veriState)
                                            {
                                                if (countO == veriState.Count())
                                                {
                                                    TypeOwners = TypeOwners + item.Name + ".";
                                                }
                                                else
                                                {
                                                    if (countO == 1)
                                                    {
                                                        TypeOwners = item.Name;
                                                    }
                                                    else
                                                    {
                                                        TypeOwners = TypeOwners + TypeOwnersE + item.Name + TypeOwnersE;
                                                    }
                                                    countO++;
                                                }
                                            }
                                            DatosMalos.FarmStatusId = "Se encontro multiples resultados para la variable: '" + r[9].ToString() + "' por favor escoger con exactitud una de estas opciones: '" + TypeOwners + "'";
                                            r[9] = DatosMalos.FarmStatusId;
                                            C_Bad++;
                                        }
                                        else
                                        {
                                            string IdState = veriState.Select(x => x.Id.ToString()).FirstOrDefault();
                                            r[9] = IdState;
                                            C_Good++;
                                        }
                                    }

                                }
                                count++;

                                //INSERTAR BUENAS A DATATABLE
                                if (C_Good == 6 && C_Bad == 0)
                                {
                                    DataRow drG = data_Good.NewRow();
                                    data_Good.ImportRow(r);
                                }
                                //INSERTAR MALAS A DATATABLE
                                else if (C_Bad > 0)
                                {
                                    DataRow drB = data_Bad.NewRow();
                                    data_Bad.ImportRow(r);
                                    DatosMalos.Code = C_F2.Trim();
                                    FieldsE.Add(DatosMalos);
                                }

                                C_Good = 0;
                                C_Bad = 0;

                            }

                            if (data_Bad.Rows.Count > 0)
                            {
                                ViewBag.DataTableE = FieldsE;
                                ViewBag.F_Bad = data_Bad.Rows.Count;
                                ViewBag.Description = "Se encontraron errores en el excel.";


                                Session["Errores"] = FieldsE;
                                //ToExcel(FieldsE);
                            }

                            if (data_Good.Rows.Count > 0)
                            {
                                List<Parametros> lstParametros = new List<Parametros>();
                                lstParametros.Add(new Parametros("@ResultadoOUTPUT", SqlDbType.NVarChar, 2000));
                                lstParametros.Add(new Parametros("@tablaTemp", data_Good));

                                //var test = await _driver.EjecutarSp("Upload_Data_ParaPruebas", lstParametros);
                                var test = await _driver.EjecutarSp("Upload_Data", lstParametros);


                                if (test.Count == 0)
                                {
                                    string result = lstParametros[0].Valor.ToString();
                                    string[] respuesta = lstParametros[0].Valor.ToString().Split(',');

                                    ViewBag.Status = null;
                                    ViewBag.Description = null;


                                    if (respuesta[0] != null && respuesta[1] != null && respuesta[2] != null)
                                    {
                                        ViewBag.Status = respuesta[0];
                                        ViewBag.Description = respuesta[1];
                                        ViewBag.F_Goods = data_Good.Rows.Count;
                                        ViewBag.F_Bad = data_Bad.Rows.Count;
                                    }

                                }
                                else
                                {
                                    ViewBag.Description = "Hubo un error en el procedimiento: " + test[0].Description;
                                    ViewBag.LineNumber = test[0].LineNumber;
                                    ViewBag.Procedure = test[0].Procedure;
                                    ViewBag.F_Goods = 0;
                                    ViewBag.F_Bad = data_Bad.Rows.Count;
                                }
                            }
                        }
                    }

                }
            }
            return View("InterfaceFarms");
        }


        public void ToExcelPlantations()
        {
            List<Plantation_Error> bads = Session["ErroresPlantations"] as List<Plantation_Error>;

            if (bads != null)
            {
                var GridView1 = new GridView();

                Plantation_Error DatosMalos = new Plantation_Error();

                //Se crea un objeto de lista vacia 
                List<Plantation_Error> ListEmpty = new List<Plantation_Error>();
                Plantation_Error Empty = new Plantation_Error();


                //se le asigna a la lista los datos que vienen de la base de datos
                List<Plantation_Error> data = bads;

                //se crea la lista vacia
                ListEmpty.Add(Empty);

                //se valida si hay datos
                if (data.Count() > 0)
                {
                    //si hay datos se agregan los datos a la lista
                    GridView1.DataSource = data.ToList();
                }
                else
                {
                    //si no se crea la lista vacia
                    GridView1.DataSource = ListEmpty.ToList();
                }

                //se crea esto para el excel
                Response.AddHeader("content-disposition", "attachment; filename=Errores Plantaciones.xls");
                Response.ClearContent();
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

                GridView1.AllowPaging = false;
                GridView1.DataBind();

                //se crea el GridViewRow para las cabeceras
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                HeaderRow.Font.Bold = true;
                HeaderRow.HorizontalAlign = HorizontalAlign.Left;

                //tablecell para los titulos de las columnas
                TableCell FILA = new TableCell();
                FILA.Text = "# FILA";
                FILA.ColumnSpan = 1;
                FILA.RowSpan = 2;
                FILA.Style.Add("background-color", "#4F6228");
                FILA.Style.Add("width", "auto");
                FILA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(FILA);

                TableCell CODIGO = new TableCell();
                CODIGO.Text = "CODIGO FINCA";
                CODIGO.ColumnSpan = 1;
                CODIGO.RowSpan = 2;
                CODIGO.Style.Add("background-color", "#4F6228");
                CODIGO.Style.Add("width", "auto");
                CODIGO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(CODIGO);

                TableCell HECTAREAS = new TableCell();
                HECTAREAS.Text = "HECTAREAS";
                HECTAREAS.ColumnSpan = 1;
                HECTAREAS.RowSpan = 2;
                HECTAREAS.Style.Add("background-color", "#4F6228");
                HECTAREAS.Style.Add("width", "auto");
                HECTAREAS.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(HECTAREAS);

                TableCell PRODUCCION_ESTIMADA = new TableCell();
                PRODUCCION_ESTIMADA.Text = "PRODUCCION ESTIMADA";
                PRODUCCION_ESTIMADA.ColumnSpan = 1;
                PRODUCCION_ESTIMADA.RowSpan = 2;
                PRODUCCION_ESTIMADA.Style.Add("background-color", "#4F6228");
                PRODUCCION_ESTIMADA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(PRODUCCION_ESTIMADA);

                TableCell EDAD = new TableCell();
                EDAD.Text = "EDAD (DD/MM/YYYY)";
                EDAD.ColumnSpan = 1;
                EDAD.RowSpan = 2;
                EDAD.Style.Add("background-color", "#4F6228");
                EDAD.Style.Add("width", "auto");
                EDAD.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(EDAD);

                TableCell E_Plant = new TableCell();
                E_Plant.Text = "E.DE LA PLANTACION (BUENO,REGULAR,MALO,NO APLICA)";
                E_Plant.ColumnSpan = 1;
                E_Plant.RowSpan = 2;
                E_Plant.Style.Add("background-color", "#4F6228");
                E_Plant.Style.Add("width", "auto");
                E_Plant.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(E_Plant);

                TableCell T_Plant = new TableCell();
                T_Plant.Text = "TIPO DE PLANTACION";
                T_Plant.ColumnSpan = 1;
                T_Plant.RowSpan = 2;
                T_Plant.Style.Add("background-color", "#4F6228");
                T_Plant.Style.Add("width", "auto");
                T_Plant.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(T_Plant);

                TableCell VARIEDAD = new TableCell();
                VARIEDAD.Text = "VARIEDAD";
                VARIEDAD.ColumnSpan = 1;
                VARIEDAD.RowSpan = 2;
                VARIEDAD.Style.Add("background-color", "#4F6228");
                VARIEDAD.Style.Add("width", "auto");
                VARIEDAD.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(VARIEDAD);

                TableCell D_ARB = new TableCell();
                D_ARB.Text = "DISTANCIA ENTRE ARBOLES";
                D_ARB.ColumnSpan = 1;
                D_ARB.RowSpan = 2;
                D_ARB.Style.Add("background-color", "#4F6228");
                D_ARB.Style.Add("width", "auto");
                D_ARB.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(D_ARB);

                TableCell D_SUR = new TableCell();
                D_SUR.Text = "DISTANCIA ENTRE SURCOS";
                D_SUR.ColumnSpan = 1;
                D_SUR.RowSpan = 2;
                D_SUR.Style.Add("background-color", "#4F6228");
                D_SUR.Style.Add("width", "auto");
                D_SUR.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(D_SUR);

                TableCell DENSIDAD = new TableCell();
                DENSIDAD.Text = "DENSIDAD";
                DENSIDAD.ColumnSpan = 1;
                DENSIDAD.RowSpan = 2;
                DENSIDAD.Style.Add("background-color", "#4F6228");
                DENSIDAD.Style.Add("width", "auto");
                DENSIDAD.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(DENSIDAD);

                TableCell N_Plants = new TableCell();
                N_Plants.Text = "N.DE PLANTAS";
                N_Plants.ColumnSpan = 1;
                N_Plants.RowSpan = 2;
                N_Plants.Style.Add("background-color", "#4F6228");
                N_Plants.Style.Add("width", "auto");
                N_Plants.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(N_Plants);

                //NUEVOS CAMPOS
                TableCell N_Lote = new TableCell();
                N_Lote.Text = "NUMERO DEL LOTE";
                N_Lote.ColumnSpan = 1;
                N_Lote.RowSpan = 2;
                N_Lote.Style.Add("background-color", "#4F6228");
                N_Lote.Style.Add("width", "auto");
                N_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(N_Lote);

                TableCell Muni_Lote = new TableCell();
                Muni_Lote.Text = "MUNICIPIO DEL LOTE";
                Muni_Lote.ColumnSpan = 1;
                Muni_Lote.RowSpan = 2;
                Muni_Lote.Style.Add("background-color", "#4F6228");
                Muni_Lote.Style.Add("width", "auto");
                Muni_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(Muni_Lote);

                TableCell Vill_Lote = new TableCell();
                Vill_Lote.Text = "VEREDA DEL LOTE";
                Vill_Lote.ColumnSpan = 1;
                Vill_Lote.RowSpan = 2;
                Vill_Lote.Style.Add("background-color", "#4F6228");
                Vill_Lote.Style.Add("width", "auto");
                Vill_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(Vill_Lote);

                TableCell Nomb_Lote = new TableCell();
                Nomb_Lote.Text = "NOMBRE DEL LOTE";
                Nomb_Lote.ColumnSpan = 1;
                Nomb_Lote.RowSpan = 2;
                Nomb_Lote.Style.Add("background-color", "#4F6228");
                Nomb_Lote.Style.Add("width", "auto");
                Nomb_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(Nomb_Lote);

                TableCell Labo_Lote = new TableCell();
                Labo_Lote.Text = "LABOR DEL LOTE (SIEMBRA, NUEVA SIEMBRA, ZOCA, NO APLICA)";
                Labo_Lote.ColumnSpan = 1;
                Labo_Lote.RowSpan = 2;
                Labo_Lote.Style.Add("background-color", "#4F6228");
                Labo_Lote.Style.Add("width", "auto");
                Labo_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(Labo_Lote);

                TableCell TIPO_Lote = new TableCell();
                TIPO_Lote.Text = "TIPO DEL LOTE (TECNIFICADO, TRADICIONAL)";
                TIPO_Lote.ColumnSpan = 1;
                TIPO_Lote.RowSpan = 2;
                TIPO_Lote.Style.Add("background-color", "#4F6228");
                TIPO_Lote.Style.Add("width", "auto");
                TIPO_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(TIPO_Lote);

                TableCell FORMA_Lote = new TableCell();
                FORMA_Lote.Text = "FORMA DEL LOTE (TRIANGULO, CUADRADO)";
                FORMA_Lote.ColumnSpan = 1;
                FORMA_Lote.RowSpan = 2;
                FORMA_Lote.Style.Add("background-color", "#4F6228");
                FORMA_Lote.Style.Add("width", "auto");
                FORMA_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(FORMA_Lote);

                TableCell N_EJES_Lote = new TableCell();
                N_EJES_Lote.Text = "# DE EJES DEL LOTE (1, 2)";
                N_EJES_Lote.ColumnSpan = 1;
                N_EJES_Lote.RowSpan = 2;
                N_EJES_Lote.Style.Add("background-color", "#4F6228");
                N_EJES_Lote.Style.Add("width", "auto");
                N_EJES_Lote.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(N_EJES_Lote);

                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];

                    //OCULATAR TITULOS
                    GridView1.HeaderRow.Cells[0].Visible = false;
                    GridView1.HeaderRow.Cells[1].Visible = false;
                    GridView1.HeaderRow.Cells[2].Visible = false;
                    GridView1.HeaderRow.Cells[3].Visible = false;
                    GridView1.HeaderRow.Cells[4].Visible = false;
                    GridView1.HeaderRow.Cells[5].Visible = false;
                    GridView1.HeaderRow.Cells[6].Visible = false;
                    GridView1.HeaderRow.Cells[7].Visible = false;
                    GridView1.HeaderRow.Cells[8].Visible = false;
                    GridView1.HeaderRow.Cells[9].Visible = false;
                    GridView1.HeaderRow.Cells[10].Visible = false;
                    GridView1.HeaderRow.Cells[11].Visible = false;

                    //nuevos campos
                    GridView1.HeaderRow.Cells[11].Visible = false;
                    GridView1.HeaderRow.Cells[12].Visible = false;
                    GridView1.HeaderRow.Cells[13].Visible = false;
                    GridView1.HeaderRow.Cells[14].Visible = false;
                    GridView1.HeaderRow.Cells[15].Visible = false;
                    GridView1.HeaderRow.Cells[16].Visible = false;
                    GridView1.HeaderRow.Cells[17].Visible = false;
                    GridView1.HeaderRow.Cells[18].Visible = false;
                    GridView1.HeaderRow.Cells[19].Visible = false;

                }

                GridView1.RenderControl(htmlTextWriter);
                //style to format numbers to string
                string style = @"<style> .NN { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public void ToExcelFamily()
        {
            List<Family_Error> bads = Session["ErroresFamily"] as List<Family_Error>;

            if (bads != null)
            {
                var GridView1 = new GridView();

                Family_Error DatosMalos = new Family_Error();

                //Se crea un objeto de lista vacia 
                List<Family_Error> ListEmpty = new List<Family_Error>();
                Family_Error Empty = new Family_Error();


                //se le asigna a la lista los datos que vienen de la base de datos
                List<Family_Error> data = bads;

                //se crea la lista vacia
                ListEmpty.Add(Empty);

                //se valida si hay datos
                if (data.Count() > 0)
                {
                    //si hay datos se agregan los datos a la lista
                    GridView1.DataSource = data.ToList();
                }
                else
                {
                    //si no se crea la lista vacia
                    GridView1.DataSource = ListEmpty.ToList();
                }

                //se crea esto para el excel
                Response.AddHeader("content-disposition", "attachment; filename=Errores Familiares.xls");
                Response.ClearContent();
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

                GridView1.AllowPaging = false;
                GridView1.DataBind();

                //se crea el GridViewRow para las cabeceras
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                HeaderRow.Font.Bold = true;
                HeaderRow.HorizontalAlign = HorizontalAlign.Left;

                //tablecell para los titulos de las columnas
                TableCell FILA = new TableCell();
                FILA.Text = "# FILA";
                FILA.ColumnSpan = 1;
                FILA.RowSpan = 2;
                FILA.Style.Add("background-color", "#4F6228");
                FILA.Style.Add("width", "auto");
                FILA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(FILA);

                TableCell CODIGO = new TableCell();
                CODIGO.Text = "CODIGO";
                CODIGO.ColumnSpan = 1;
                CODIGO.RowSpan = 2;
                CODIGO.Style.Add("background-color", "#4F6228");
                CODIGO.Style.Add("width", "auto");
                CODIGO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(CODIGO);

                TableCell NOMBRES = new TableCell();
                NOMBRES.Text = "NOMBRES";
                NOMBRES.ColumnSpan = 1;
                NOMBRES.RowSpan = 2;
                NOMBRES.Style.Add("background-color", "#4F6228");
                NOMBRES.Style.Add("width", "auto");
                NOMBRES.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(NOMBRES);

                TableCell APELLIDOS = new TableCell();
                APELLIDOS.Text = "APELLIDOS";
                APELLIDOS.ColumnSpan = 1;
                APELLIDOS.RowSpan = 2;
                APELLIDOS.Style.Add("background-color", "#4F6228");
                APELLIDOS.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(APELLIDOS);

                TableCell F_NACI = new TableCell();
                F_NACI.Text = "F. NACIMIENTO (DD/MM/YYYY)";
                F_NACI.ColumnSpan = 1;
                F_NACI.RowSpan = 2;
                F_NACI.Style.Add("background-color", "#4F6228");
                F_NACI.Style.Add("width", "auto");
                F_NACI.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(F_NACI);

                TableCell CEDULA = new TableCell();
                CEDULA.Text = "CEDULA";
                CEDULA.ColumnSpan = 1;
                CEDULA.RowSpan = 2;
                CEDULA.Style.Add("background-color", "#4F6228");
                CEDULA.Style.Add("width", "auto");
                CEDULA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(CEDULA);

                TableCell EDUCACION = new TableCell();
                EDUCACION.Text = "EDUCACION";
                EDUCACION.ColumnSpan = 1;
                EDUCACION.RowSpan = 2;
                EDUCACION.Style.Add("background-color", "#4F6228");
                EDUCACION.Style.Add("width", "auto");
                EDUCACION.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(EDUCACION);

                TableCell RELACION = new TableCell();
                RELACION.Text = "RELACION";
                RELACION.ColumnSpan = 1;
                RELACION.RowSpan = 2;
                RELACION.Style.Add("background-color", "#4F6228");
                RELACION.Style.Add("width", "auto");
                RELACION.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(RELACION);

                TableCell E_CIVIL = new TableCell();
                E_CIVIL.Text = "E. CIVIL";
                E_CIVIL.ColumnSpan = 1;
                E_CIVIL.RowSpan = 2;
                E_CIVIL.Style.Add("background-color", "#4F6228");
                E_CIVIL.Style.Add("width", "auto");
                E_CIVIL.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(E_CIVIL);

                TableCell PROPIETARIO = new TableCell();
                PROPIETARIO.Text = "PROPIETARIO";
                PROPIETARIO.ColumnSpan = 1;
                PROPIETARIO.RowSpan = 2;
                PROPIETARIO.Style.Add("background-color", "#4F6228");
                PROPIETARIO.Style.Add("width", "auto");
                PROPIETARIO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(PROPIETARIO);

                TableCell TELEFONO = new TableCell();
                TELEFONO.Text = "TELEFONO";
                TELEFONO.ColumnSpan = 1;
                TELEFONO.RowSpan = 2;
                TELEFONO.Style.Add("background-color", "#4F6228");
                TELEFONO.Style.Add("width", "auto");
                TELEFONO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(TELEFONO);

                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];

                    //OCULATAR TITULOS
                    GridView1.HeaderRow.Cells[0].Visible = false;
                    GridView1.HeaderRow.Cells[1].Visible = false;
                    GridView1.HeaderRow.Cells[2].Visible = false;
                    GridView1.HeaderRow.Cells[3].Visible = false;
                    GridView1.HeaderRow.Cells[4].Visible = false;
                    GridView1.HeaderRow.Cells[5].Visible = false;
                    GridView1.HeaderRow.Cells[6].Visible = false;
                    GridView1.HeaderRow.Cells[7].Visible = false;
                    GridView1.HeaderRow.Cells[8].Visible = false;
                    GridView1.HeaderRow.Cells[9].Visible = false;
                    GridView1.HeaderRow.Cells[10].Visible = false;
                }

                GridView1.RenderControl(htmlTextWriter);
                //style to format numbers to string
                string style = @"<style> .NN { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public void ToExcel()
        {
            List<F_Error> bads = Session["Errores"] as List<F_Error>;

            if (bads != null)
            {
                var GridView1 = new GridView();

                F_Error DatosMalos = new F_Error();

                //Se crea un objeto de lista vacia 
                List<F_Error> ListEmpty = new List<F_Error>();
                F_Error Empty = new F_Error();


                //se le asigna a la lista los datos que vienen de la base de datos
                List<F_Error> data = bads;

                //se crea la lista vacia
                ListEmpty.Add(Empty);

                //se valida si hay datos
                if (data.Count() > 0)
                {
                    //si hay datos se agregan los datos a la lista
                    GridView1.DataSource = data.ToList();
                }
                else
                {
                    //si no se crea la lista vacia
                    GridView1.DataSource = ListEmpty.ToList();
                }

                //se crea esto para el excel
                Response.AddHeader("content-disposition", "attachment; filename=Errores Fincas.xls");
                Response.ClearContent();
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

                GridView1.AllowPaging = false;
                GridView1.DataBind();

                //se crea el GridViewRow para las cabeceras
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                HeaderRow.Font.Bold = true;
                HeaderRow.HorizontalAlign = HorizontalAlign.Left;

                //tablecell para los titulos de las columnas
                TableCell FILA = new TableCell();
                FILA.Text = "# FILA";
                FILA.ColumnSpan = 1;
                FILA.RowSpan = 2;
                FILA.Style.Add("background-color", "#4F6228");
                FILA.Style.Add("width", "auto");
                FILA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(FILA);

                TableCell NOMBRE = new TableCell();
                NOMBRE.Text = "NOMBRE";
                NOMBRE.ColumnSpan = 1;
                NOMBRE.RowSpan = 2;
                NOMBRE.Style.Add("background-color", "#4F6228");
                NOMBRE.Style.Add("width", "auto");
                NOMBRE.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(NOMBRE);

                TableCell CODIGO = new TableCell();
                CODIGO.Text = "CODIGO";
                CODIGO.ColumnSpan = 1;
                CODIGO.RowSpan = 2;
                CODIGO.Style.Add("background-color", "#4F6228");
                CODIGO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(CODIGO);

                TableCell LATITUD = new TableCell();
                LATITUD.Text = "LATITUD";
                LATITUD.ColumnSpan = 1;
                LATITUD.RowSpan = 2;
                LATITUD.Style.Add("background-color", "#4F6228");
                LATITUD.Style.Add("width", "auto");
                LATITUD.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(LATITUD);

                TableCell LONGITUD = new TableCell();
                LONGITUD.Text = "LONGITUD";
                LONGITUD.ColumnSpan = 1;
                LONGITUD.RowSpan = 2;
                LONGITUD.Style.Add("background-color", "#4F6228");
                LONGITUD.Style.Add("width", "auto");
                LONGITUD.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(LONGITUD);

                TableCell MUNICIPIO = new TableCell();
                MUNICIPIO.Text = "MUNICIPIO";
                MUNICIPIO.ColumnSpan = 1;
                MUNICIPIO.RowSpan = 2;
                MUNICIPIO.Style.Add("background-color", "#4F6228");
                MUNICIPIO.Style.Add("width", "auto");
                MUNICIPIO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(MUNICIPIO);

                TableCell VEREDA = new TableCell();
                VEREDA.Text = "VEREDA";
                VEREDA.ColumnSpan = 1;
                VEREDA.RowSpan = 2;
                VEREDA.Style.Add("background-color", "#4F6228");
                VEREDA.Style.Add("width", "auto");
                VEREDA.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(VEREDA);

                TableCell TIPO_PROPIETARIO = new TableCell();
                TIPO_PROPIETARIO.Text = "TIPO PROPIETARIO";
                TIPO_PROPIETARIO.ColumnSpan = 1;
                TIPO_PROPIETARIO.RowSpan = 2;
                TIPO_PROPIETARIO.Style.Add("background-color", "#4F6228");
                TIPO_PROPIETARIO.Style.Add("width", "auto");
                TIPO_PROPIETARIO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(TIPO_PROPIETARIO);

                TableCell ORGANIZACION = new TableCell();
                ORGANIZACION.Text = "ADMINISTRADOR ORGANIZACION";
                ORGANIZACION.ColumnSpan = 1;
                ORGANIZACION.RowSpan = 2;
                ORGANIZACION.Style.Add("background-color", "#4F6228");
                ORGANIZACION.Style.Add("width", "auto");
                ORGANIZACION.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(ORGANIZACION);

                TableCell ADMIN = new TableCell();
                ADMIN.Text = "ADMINISTRADOR PROGRAMA";
                ADMIN.ColumnSpan = 1;
                ADMIN.RowSpan = 2;
                ADMIN.Style.Add("background-color", "#4F6228");
                ADMIN.Style.Add("width", "auto");
                ADMIN.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(ADMIN);

                TableCell ESTADO = new TableCell();
                ESTADO.Text = "ESTADO OASIS (A-R-F-NA)";
                ESTADO.ColumnSpan = 1;
                ESTADO.RowSpan = 2;
                ESTADO.Style.Add("background-color", "#4F6228");
                ESTADO.Style.Add("width", "auto");
                ESTADO.Style.Add("color", "#FFFFFF");
                HeaderRow.Cells.Add(ESTADO);

                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];

                    //OCULATAR TITULOS
                    GridView1.HeaderRow.Cells[0].Visible = false;
                    GridView1.HeaderRow.Cells[1].Visible = false;
                    GridView1.HeaderRow.Cells[2].Visible = false;
                    GridView1.HeaderRow.Cells[3].Visible = false;
                    GridView1.HeaderRow.Cells[4].Visible = false;
                    GridView1.HeaderRow.Cells[5].Visible = false;
                    GridView1.HeaderRow.Cells[6].Visible = false;
                    GridView1.HeaderRow.Cells[7].Visible = false;
                    GridView1.HeaderRow.Cells[8].Visible = false;
                    GridView1.HeaderRow.Cells[9].Visible = false;
                    GridView1.HeaderRow.Cells[10].Visible = false;

                }

                GridView1.RenderControl(htmlTextWriter);
                //style to format numbers to string
                string style = @"<style> .NN { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public bool VerifyPossitive(string Verify)
        {
            var poss = Verify.ToArray();

            if (poss[0] >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerifyLess(string Verify)
        {
            var less = Verify.ToArray();

            if (less[0].ToString().Contains("-"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerifyLetters(string Verify)
        {
            int temp = 0;
            Verify = Verify.Replace(".", "");
            Verify = Verify.Replace(",", "");

            if (int.TryParse(Verify, out temp))
            {
                return false;
            }
            else
            {
                if (Verify.Contains("1") || Verify.Contains("2") || Verify.Contains("3") || Verify.Contains("4") || Verify.Contains("5") || Verify.Contains("6") || Verify.Contains("7") || Verify.Contains("8") || Verify.Contains("9") || Verify.Contains("0"))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public bool VerifyNull(string Verify)
        {
            if (Verify == null || Verify.Length == 0 || Verify == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerifyNumber(string Verify)
        {
            Int64 ejem = 0;

            if (Int64.TryParse(Verify, out ejem))
            {
                return true;
            }
            else
            {
                var partido = Verify.Split('-');
                if (Int64.TryParse(partido[0], out ejem))
                {
                    if (Int64.TryParse(partido[1], out ejem))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }

        public bool Verifyfloat(string Verify)
        {
            float ejem = 0;

            if (float.TryParse(Verify, out ejem))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath, Encoding.UTF7);
            string[] headers = sr.ReadLine().Split(';');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ";(?=(:[^\"]*\"[^\"]*\")*[^\"]*$)");

                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }

            sr.Close();

            return dt;
        }


        public class RowError
        {
            public int Fila { get; set; }
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
        }


        public class PlantationsT
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid PlantationTypeId { get; set; }

        }

        public class Family_Error
        {
            public int Fila { get; set; }
            public string Codigo_F { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string F_naci { get; set; }
            public string Cedula { get; set; }
            public string Educacion { get; set; }
            public string Relacion { get; set; }
            public string E_Civil { get; set; }
            public string Propietario { get; set; }
            public string Telefono { get; set; }
        }

        public class Plantation_Error
        {
            public int Fila { get; set; }
            public string Codigo_F { get; set; }
            public string Hectareas { get; set; }
            public string P_Estimada { get; set; }
            public string Edad { get; set; }
            public string N_Plantas { get; set; }
            public string Est_Plantacion { get; set; }
            public string Tip_Plantacion { get; set; }
            public string Variedad { get; set; }
            public string Distancia_ARB { get; set; }
            public string Distancia_SUR { get; set; }
            public string Densidad { get; set; }

            //nuevos campos
            public string Codigo_lot { get; set; }
            public string Muni_lot { get; set; }
            public string Vill_lot { get; set; }
            public string Nomb_lot { get; set; }
            public string Labor_lot { get; set; }
            public string Tip_lot { get; set; }
            public string Form_lot { get; set; }
            public string NumEjes_lot { get; set; }
        }

        public class CedulaList
        {
            public string Cedula { get; set; }
        }

        public class F_Error
        {

            public int Fila { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Lat { get; set; }
            public string Long { get; set; }
            [NotMapped]
            public string MunicipalityId { get; set; }
            public string VillageId { get; set; }
            public string OwnershipTypeId { get; set; }
            public string Organizacion { get; set; }
            public string SupplyChainId { get; set; }
            public string FarmStatusId { get; set; }
        }

    }
}
