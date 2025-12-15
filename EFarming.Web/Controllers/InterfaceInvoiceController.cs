using EFarming.Core;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DAL;
using Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    public class InterfaceInvoiceController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// The _storage
        /// </summary>
        private IStorage _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceInvoiceController"/> class.
        /// </summary>
        /// <param name="storage"> the storage</param>
        public InterfaceInvoiceController(Storage storage)
        {
            _storage = storage;
        }

        // GET: InterfaceInvoice
        public ActionResult IndexCoffee()
        {
            return View();
        }

        public ActionResult IndexFertilizer()
        {
            return View();
        }

        public ActionResult DetailCoffee()
        {
            return View();
        }

        public ActionResult DetailFertilizer()
        {
            return View();
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="excelfile">The excelfile.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadInvoiceCoffee(HttpPostedFileBase excelfile)
        {
            if (excelfile != null)
            {
                List<List<object>> loadResults = new List<List<object>>();
                if (excelfile.ContentLength > 0)
                {
                    Guid id = Guid.NewGuid();

                    string fileName = id.ToString() + excelfile.FileName.Substring(excelfile.FileName.LastIndexOf("."));
                    string savedFileName = "~/Content/InterfaceInvoiceCoffee/" + excelfile.FileName;
                    excelfile.SaveAs(Server.MapPath(savedFileName));

                    string blobContainerName = ConfigurationManager.AppSettings["StorageInvoicesContainer"];
                    string contentTypeFile = string.Empty;
                    byte[] bytesFile;

                    string uriFile = string.Empty;

                    int size = excelfile.ContentLength;

                    bytesFile = null;
                    using (var binaryReader = new BinaryReader(excelfile.InputStream))
                    {
                        bytesFile = binaryReader.ReadBytes(excelfile.ContentLength);
                    }
                    contentTypeFile = MimeMapping.GetMimeMapping(excelfile.FileName);
                    uriFile = _storage.UploadToBlob(blobContainerName, "Caficauca/" + fileName, bytesFile, contentTypeFile);

                    loadResults = UploadDataToDatabase(excelfile.FileName, 1);
                }
                if (loadResults == null || loadResults.Count() == 0)
                {
                    ViewBag.message = "EL ARCHIVO NO SE CARGÓ, POR FAVOR VERIFIQUE QUE ESTE CARGANDO EL ARCHIVO INDICADO O QUE LA INFORMACIÓN A CARGAR CUMPLA CON LAS RECOMENDACIONES Y FORMATO DE DATOS ESTABLECIDOS.";
                    return View("IndexCoffee", loadResults);
                }
                else if (loadResults.Count() > 0)
                {
                    //ViewBag.message = "EL ARCHIVO SE CARGO CORRECTAMENTE, REGISTROS INSERTADOS " + loadResults.Count() + ".";
                    return View("DetailCoffee", loadResults);
                }
                else
                {
                    return View("IndexCoffee", loadResults);
                }

            }
            else
            {
                return View("IndexCoffee");
            }
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="excelfile">The excelfile.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadInvoiceFertilizer(HttpPostedFileBase excelfile)
        {
            if (excelfile != null)
            {
                List<List<object>> loadResults = new List<List<object>>();
                if (excelfile.ContentLength > 0)
                {
                    string savedFileName = "~/Content/InterfaceInvoiceFertilizer/" + excelfile.FileName;
                    excelfile.SaveAs(Server.MapPath(savedFileName));

                    Guid id = Guid.NewGuid();

                    string fileName = id.ToString() + excelfile.FileName.Substring(excelfile.FileName.LastIndexOf("."));


                    string blobContainerName = ConfigurationManager.AppSettings["StorageInvoicesContainer"];
                    string contentTypeFile = string.Empty;
                    byte[] bytesFile;

                    string uriFile = string.Empty;

                    int size = excelfile.ContentLength;

                    bytesFile = null;
                    using (var binaryReader = new BinaryReader(excelfile.InputStream))
                    {
                        bytesFile = binaryReader.ReadBytes(excelfile.ContentLength);
                    }
                    contentTypeFile = MimeMapping.GetMimeMapping(excelfile.FileName);
                    uriFile = _storage.UploadToBlob(blobContainerName, "Caficauca/" + fileName, bytesFile, contentTypeFile);


                    loadResults = UploadDataToDatabase(excelfile.FileName, 2);
                }
                if (loadResults == null || loadResults.Count() == 0)
                {
                    ViewBag.message = "EL ARCHIVO NO SE CARGÓ, POR FAVOR VERIFIQUE QUE ESTE CARGANDO EL ARCHIVO INDICADO O QUE LA INFORMACIÓN A CARGAR CUMPLA CON LAS RECOMENDACIONES Y FORMATO DE DATOS ESTABLECIDOS.";
                    return View("IndexFertilizer", loadResults);
                }
                else if (loadResults.Count() > 0)
                {
                    //ViewBag.message = "EL ARCHIVO SE CARGO CORRECTAMENTE, REGISTROS INSERTADOS " + loadResults.Count() + ".";
                    return View("DetailFertilizer", loadResults);
                }
                else
                {
                    return View("IndexFertilizer", loadResults);
                }
            }
            else
            {
                return View("IndexFertilizer");
            }
        }

        /// <summary>
        /// Uploads the data to database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<List<object>> UploadDataToDatabase(string FileName, int type)
        {
            try
            {
                int ExtensionDot = FileName.IndexOf(".");
                string extension = FileName.Substring(ExtensionDot);
                string filePath = "";
                if (type == 1)
                {
                    filePath = Path.Combine(Server.MapPath("/Content/InterfaceInvoiceCoffee/"), FileName);
                }
                else if (type == 2)
                {
                    filePath = Path.Combine(Server.MapPath("/Content/InterfaceInvoiceFertilizer/"), FileName);
                }

                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                IExcelDataReader excelReader = null;
                if (extension == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (extension == ".xlsx")
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                };

                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                //DataSet result = excelReader.AsDataSet();

                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;

                DataSet result = excelReader.AsDataSet();
                int CountCaptionsInvoiceCoffee = 0;
                int CountCaptionsInvoiceFertilizer = 0;
                string validation;

                //var EndDate = DateTime.Now.AddDays(-1);
                //var DateInvoice = Convert.ToDateTime("01/01/1900");

                List<List<object>> loadResults = new List<List<object>>();
                switch (type)
                {
                    case 1:
                        foreach (DataColumn column in result.Tables[0].Columns)
                        {
                            System.Console.WriteLine(column.Caption);
                            switch (column.Caption)
                            {
                                case "CODIGO DEL ASOCIADO":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "CANTIDAD":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "FECHA FACTURA":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "NÚMERO FACTURA":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "VALOR BRUTO":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "UBICACIÓN":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "BODEGA":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "CAJA":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "PRECIO":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                case "CÓDIGO DEL PRODUCTO":
                                    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    break;
                                    //case "TIPO DE DOCUMENTO":
                                    //    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    //    break;
                                    //case "ES SOCIO?  S/N":
                                    //    CountCaptionsInvoiceCoffee = CountCaptionsInvoiceCoffee + 1;
                                    //    break;
                            };
                        };
                        // 7 are the minimun number of parameters necesary to upload the information to database. 
                        if (CountCaptionsInvoiceCoffee == 10)
                        {
                            //var invoices = db.Invoices.OrderByDescending(d => d.Date);
                            //DateTime LastInvoiceDate = DateTime.Now.Date;
                            //if (invoices.Count() > 0)
                            //{
                            //    LastInvoiceDate = db.Invoices.OrderByDescending(d => d.Date).First().Date;
                            //}
                            //LastInvoiceDate = LastInvoiceDate.AddDays(1);

                            foreach (DataRow row in result.Tables[0].Rows)
                            {

                                if (row[0].ToString() != "" && row[3].ToString() != "")
                                {
                                    if (row[2].ToString() != "")
                                    {
                                        CreateInvoiceCoffee(loadResults, row);
                                        //if (row[10].ToString().ToUpper() == "CF")
                                        //{
                                        //    if (row[11].ToString().ToUpper() == "S")
                                        //    {
                                        //        CreateInvoiceCoffee(loadResults, row);
                                        //    }
                                        //    else
                                        //    {
                                        //        loadResults.Add(new List<object> { "IsNotPartner", row[0].ToString(), row[3].ToString(), row[11].ToString() });
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    loadResults.Add(new List<object> { "DocumentTypeNotValid", row[0].ToString(), row[3].ToString(), row[10].ToString() });
                                        //}
                                    }
                                    else
                                    {
                                        var dt = Convert.ToDateTime("01/01/1900");
                                        loadResults.Add(new List<object> { "ErrorRegistry", row[0].ToString(), row[3].ToString(), dt });
                                        //loadResults.Add(new List<object> { "ErrorRegistry", row[0].ToString(), row[3].ToString(), Convert.ToDateTime(row[2].ToString()) });
                                    }
                                }
                                else
                                {
                                    loadResults.Add(new List<object> { "ErrorRegistry", row[0].ToString(), row[3].ToString(), Convert.ToDateTime(row[2].ToString()) });
                                }
                            }
                        }
                        else
                        {
                            // When dont find all the necesary columns in the excel file
                            validation = "NO OK";
                        };
                        break;
                    case 2:
                        foreach (DataColumn column in result.Tables[0].Columns)
                        {
                            System.Console.WriteLine(column.Caption);
                            switch (column.Caption)
                            {
                                case "CODIGO DEL ASOCIADO":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "NOMBRE DEL PRODUCTO":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "CANTIDAD":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "FECHA FACTURA":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "NÚMERO FACTURA":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "VALOR":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "UBICACIÓN":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "BODEGA":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "CAJA":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "PRECIO":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                case "CÓDIGO DE CLASIFICACIÓN DEL PRODUCTO":
                                    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    break;
                                    //case "ES SOCIO?  S/N":
                                    //    CountCaptionsInvoiceFertilizer = CountCaptionsInvoiceFertilizer + 1;
                                    //    break;
                            };
                        };
                        if (CountCaptionsInvoiceFertilizer == 11)
                        {
                            //var fertilizes = db.Fertilizers.OrderByDescending(d => d.Date);
                            //DateTime LastfertilizesDate = DateTime.Now.Date;
                            //if (fertilizes.Count() > 0)
                            //{
                            //    LastfertilizesDate = db.Fertilizers.OrderByDescending(d => d.Date).First().Date;
                            //}
                            //LastfertilizesDate = LastfertilizesDate.AddDays(1);

                            foreach (DataRow row in result.Tables[0].Rows)
                            {
                                if (row[0].ToString() != "" && row[1].ToString() != "" && row[4].ToString() != "")
                                {
                                    if (row[3].ToString() != "")
                                    {
                                        //if (row[10].ToString().ToUpper() == "204001" || row[10].ToString().ToUpper() == "903001")
                                        //{
                                        //if (row[11].ToString().ToUpper() == "S")
                                        //{
                                        CreateInvoiceFertilizers(loadResults, row);
                                        //}
                                        //else
                                        //{
                                        //    loadResults.Add(new List<object> { "IsNotPartner", row[0].ToString(), row[4].ToString(), row[11].ToString() });
                                        //}
                                        //}
                                        //else
                                        //{
                                        //    loadResults.Add(new List<object> { "ProductCodeNotFound", row[0].ToString(), row[4].ToString(), row[10].ToString() });
                                        //}
                                    }
                                    else
                                    {
                                        var dt = Convert.ToDateTime("01/01/1900");
                                        loadResults.Add(new List<object> { "ErrorRegistry", row[0].ToString(), row[1].ToString(), row[4].ToString(), dt });
                                    }
                                }
                                else
                                {
                                    loadResults.Add(new List<object> { "ErrorRegistry", row[0].ToString(), row[1].ToString(), row[4].ToString(), Convert.ToDateTime(row[3].ToString()) });
                                }
                            }
                        }
                        else
                        {
                            // When dont find all the necesary columns in the excel file
                            validation = "NO OK";
                        };
                        break;
                };
                ViewBag.loadResults = loadResults;
                excelReader.Close();
                return ViewBag.loadResults;
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return ViewBag.loadResults;
            }
        }

        public void CreateInvoiceCoffee(List<List<object>> loadResults, DataRow row)
        {
            try
            {
                Invoice _Invoice = new Invoice();
                _Invoice.Id = new Guid();
                _Invoice.Identification = row[0].ToString();
                if (row[1].ToString() != "")
                    _Invoice.Weight = float.Parse(row[1].ToString());
                else
                    _Invoice.Weight = 0;
                _Invoice.Date = DateTime.Parse(row[2].ToString());
                _Invoice.DateInvoice = DateTime.Parse(row[2].ToString());
                _Invoice.CreatedAt = DateTime.Now;
                _Invoice.InvoiceNumber = Int32.Parse(row[3].ToString());
                if (row[4].ToString() != "")
                    _Invoice.Value = Double.Parse(row[4].ToString());
                else
                    _Invoice.Value = 0;
                if (row[5].ToString() != "")
                    _Invoice.Ubication = Int32.Parse(row[5].ToString());
                else
                    _Invoice.Ubication = 0;
                if (row[6].ToString() != "")
                    _Invoice.Hold = Int32.Parse(row[6].ToString());
                else
                    _Invoice.Hold = 0;
                if (row[7].ToString() != "")
                    _Invoice.Cash = Int32.Parse(row[7].ToString());
                else
                    _Invoice.Cash = 0;
                if (row[8].ToString() != "")
                    _Invoice.BaseKg = Double.Parse(row[8].ToString());
                else
                    _Invoice.BaseKg = 0;

                CoffeeType coffetypeId = new CoffeeType();
                var coffetypeIdentifier = Int32.Parse(row[9].ToString());
                if (row[9].ToString() != "")
                    coffetypeId = db.CoffeeType.Where(t => t.Identifier == coffetypeIdentifier).FirstOrDefault();
                else
                    coffetypeId = null;

                if (coffetypeId != null)
                    _Invoice.CoffeeTypeId = coffetypeId.Id;
                else
                    loadResults.Add(new List<object> { "ProductCodeNotFound", row[0].ToString(), row[3].ToString(), row[9].ToString() });
                var verifyCoffeeType = coffetypeId.Id;

                if (CheckInvoice(_Invoice))
                {
                    var farm = db.Farms.Where(f => f.Code == _Invoice.Identification).FirstOrDefault();
                    if (farm != null)
                    {
                        _Invoice.FarmId = farm.Id;
                        db.Invoices.Add(_Invoice);
                        db.SaveChanges();
                        loadResults.Add(new List<object> { "SuccessfulRegistration", row[0].ToString() });
                    }
                    else
                    {
                        loadResults.Add(new List<object> { "FarmNotExist", row[0].ToString(), row[3].ToString() });
                    }
                }
                else
                {
                    loadResults.Add(new List<object> { "RepeatedInvoice", row[0].ToString(), row[3].ToString(), Convert.ToDateTime(row[2].ToString()) });
                }
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        public void CreateInvoiceFertilizers(List<List<object>> loadResults, DataRow row)
        {
            try
            {
                Fertilizer _Fertilizer = new Fertilizer();
                _Fertilizer.Id = new Guid();
                _Fertilizer.Identification = row[0].ToString();
                _Fertilizer.Name = row[1].ToString();
                if (row[2].ToString() != "")
                    _Fertilizer.Quantity = Int32.Parse(row[2].ToString());
                else
                    _Fertilizer.Quantity = 0;
                _Fertilizer.Date = DateTime.Parse(row[3].ToString());
                _Fertilizer.CreatedAt = DateTime.Now;
                _Fertilizer.InvoiceNumber = Int32.Parse(row[4].ToString());

                if (row[5].ToString() != "")
                    _Fertilizer.Value = Int32.Parse(row[5].ToString());
                else
                    _Fertilizer.Value = 0;
                if (row[6].ToString() != "")
                    _Fertilizer.Ubication = Int32.Parse(row[6].ToString());
                else
                    _Fertilizer.Ubication = 0;
                if (row[7].ToString() != "")
                    _Fertilizer.Hold = Int32.Parse(row[7].ToString());
                else
                    _Fertilizer.Hold = 0;
                if (row[8].ToString() != "")
                    _Fertilizer.CashRegister = Int32.Parse(row[8].ToString());
                else
                    _Fertilizer.CashRegister = 0;
                if (row[9].ToString() != "")
                    _Fertilizer.UnitPrice = Int32.Parse(row[9].ToString());
                else
                    _Fertilizer.UnitPrice = 0;

                var farm = db.Farms.Where(f => f.Code == _Fertilizer.Identification.ToString()).FirstOrDefault();
                if (farm != null)
                {
                    _Fertilizer.FarmId = farm.Id;
                    db.Fertilizers.Add(_Fertilizer);
                    db.SaveChanges();
                    loadResults.Add(new List<object> { "SuccessfulRegistration", row[0].ToString() });
                }
                else
                {
                    loadResults.Add(new List<object> { "FarmNotExist", row[0].ToString(), row[4].ToString() });
                }
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }


        public bool CheckInvoice(Invoice _invoice)
        {
            Invoice findInvoice = db.Invoices.Where(i => i.Identification == _invoice.Identification && i.InvoiceNumber == _invoice.InvoiceNumber).FirstOrDefault();
            //Invoice findInvoice = db.Invoices.Find(_invoice.InvoiceNumber);
            if (findInvoice == null)
                return true;
            else
                return false;
        }

    }
}
