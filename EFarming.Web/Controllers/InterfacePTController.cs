//using EFarming.Core.FarmModule.FarmAggregate;
//using EFarming.DTO.QualityModule;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Implementation;
//using EFarming.Repository.FarmModule;
//using EFarming.Web.Models;
//using Excel;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
 
//    /// <summary>
//    /// Interface for upload the information of quality atributes of Antioquia Department
//    /// In Antioquia the process of collect the data is diferent of the all another cooperatives
//    /// for this reason we development this interface to upload the information
//    /// </summary>
//    [CustomAuthorize(Roles = "User,Quality,Manager,Taster")]
//    public class InterfacePTController : BaseController
//    {
//        private IFarmRepository _farmRepository;
//        private ISensoryProfileManager _sensoryProfileManager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="InterfacePTController"/> class.
//        /// </summary>
//        /// <param name="farmRepository">The farm repository.</param>
//        /// <param name="sensoryProfileManager">The sensory profile manager.</param>
//        public InterfacePTController(FarmRepository farmRepository, SensoryProfileManager sensoryProfileManager)
//        {
//            _farmRepository = farmRepository;
//            _sensoryProfileManager = sensoryProfileManager;
//        }

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index(){return View();}

//        /// <summary>
//        /// Uploads the file.
//        /// </summary>
//        /// <param name="excelfile">The excelfile.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public ActionResult UploadFile(HttpPostedFileBase excelfile)
//        {
//            if (excelfile != null)
//            {
//                List<List<object>> loadResults = new List<List<object>>();
//                if (excelfile.ContentLength > 0)
//                {
//                    string savedFileName = "~/Content/InterfacePT/" + excelfile.FileName;
//                    //var path = Path.Combine(Server.MapPath("~/Content/InterfacePT"), fileName);

//                    excelfile.SaveAs(Server.MapPath(savedFileName));
//                    loadResults = UploadDataToDatabase(excelfile.FileName);
//                }
//                return View("Index", loadResults);
//            }
//            else
//            {
//                return View("Index");
//            }
//        }

//        /// <summary>
//        /// Uploads the data to database.
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public List<List<object>> UploadDataToDatabase(string FileName)
//        {
//            try
//            {
//                int ExtensionDot = FileName.IndexOf(".");
//                string extension = FileName.Substring(ExtensionDot);
//                string filePath = Path.Combine(Server.MapPath("/Content/interfacePT/"), FileName);

//                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

//                IExcelDataReader excelReader = null;
//                if (extension == ".xls")
//                {
//                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
//                }
//                else if (extension == ".xlsx")
//                {
//                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
//                };

//                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
//                //DataSet result = excelReader.AsDataSet();

//                //4. DataSet - Create column names from first row
//                excelReader.IsFirstRowAsColumnNames = true;

//                DataSet result = excelReader.AsDataSet();

//                DataTable dt = result.Tables[0];

//                int CountCaptions = 0;
//                foreach (DataColumn column in result.Tables[0].Columns)
//                {
//                    System.Console.WriteLine(column.Caption);
//                    switch (column.Caption)
//                    {
//                        case "FECHA":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "Cod Nessp":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "ATRIBUTOS NEGATIVOS":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "Aprueba Sobreprecio":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "COMENTARIOS":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "Kilos":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "CLASIFICACIÓN":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                        case "ID":
//                            CountCaptions = CountCaptions + 1;
//                            break;
//                    };
//                };

//                string validation;

//                List<List<object>> loadResults = new List<List<object>>();

//                // This is the ids of the answers that can be saved in the database
//                string[] AnswersArray = new string[] { "90A8ECC4-14A5-4CA8-8B02-354A7F85341A", "86EE4AAE-C837-43C8-A081-423747EE3DBA", "D5FE6059-256D-46DD-A03A-533876191B96", "E75FF320-9F01-4197-B139-D5F147C401CA", "71F45B44-692E-4487-8C93-3ACB7C9680F0" };

//                // This is the order of the columns in the excel file
//                int[] ColumnIndexArray = new int[] { 26, 4, 25, 10, 27 };

//                // 7 are the minimun number of parameters necesary to upload the information to database. 
//                if (CountCaptions == 8)
//                {
//                    foreach (DataRow row in result.Tables[0].Rows)
//                    {
//                        if(row[5].ToString() != "")
//                        {
//                            SaveToDataBase(loadResults, AnswersArray, ColumnIndexArray, row);
//                        }
//                    }
//                }
//                else
//                {
//                    // When dont find all the necesary columns in the excel file
//                    validation = "NO OK";
//                };

//                ViewBag.loadResults = loadResults;

//                //5. Data Reader methods
//                while (excelReader.Read())
//                {
//                    foreach (DataColumn column in result.Tables[0].Columns)
//                    {
//                        System.Console.WriteLine(column.Caption);
//                    };
//                }

//                ViewBag.Name = excelReader.Name;

//                //6. Free resources (IExcelDataReader is IDisposable)
//                excelReader.Close();

//                return ViewBag.loadResults;
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Exception = ex;
//                return ViewBag.loadResults;
//            }
//        }

//        private void SaveToDataBase(List<List<object>> loadResults, string[] AnswersArray, int[] ColumnIndexArray, DataRow row)
//        {
//            //List<object> IdExistsList = new List<object>();
//            //List<object> BadFarms = new List<object>();
//            var result1 = _farmRepository.FarmByCode(row[5].ToString());
//            if (result1.Count() > 0)
//            {
//                ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> IdExists = _sensoryProfileManager.Filter(User.UserId, row[8].ToString());
//                if (IdExists.Count() > 0)
//                {
//                    loadResults.Add(new List<object> { "IdExists", row[8].ToString() });
//                } else
//                {
//                    SensoryProfileAssessmentDTO assessment = new SensoryProfileAssessmentDTO();
//                    assessment.SensoryProfileAnswers = new List<SensoryProfileAnswerDTO>();
//                    assessment.Id = Guid.NewGuid();
//                    assessment.CreatedAt = DateTime.Now;
//                    assessment.Date = DateTime.Now;
//                    assessment.Description = row[8].ToString();

//                    assessment.FarmId = result1[0].Id;
//                    assessment.AssessmentTemplateId = new Guid("7B01B167-B114-4D6A-8174-8E45571A9216");
//                    assessment.Type = "Farm";
//                    assessment.UserId = User.UserId;

//                    int i = 0;
//                    bool BadAnswer = false;
//                    string SavedDefect = "";
//                    string SavedDecision = "";
//                    string SavedClasification = "";
//                    string SavedId = "";
//                    string SavedObservation = "";
//                    foreach (var answer in AnswersArray)
//                    {
//                        string ToSaveAnswer = row[ColumnIndexArray[i]].ToString();
//                        if (row[ColumnIndexArray[i]].ToString() != "")
//                        {
//                            if (answer.ToString() == "86EE4AAE-C837-43C8-A081-423747EE3DBA")
//                            {
//                                if (row[ColumnIndexArray[i]].ToString() == "SI")
//                                {
//                                    ToSaveAnswer = "OK";
//                                    SavedDecision = "OK";
//                                } else if (row[ColumnIndexArray[i]].ToString() == "NO")
//                                {
//                                    ToSaveAnswer = "NO OK";
//                                    SavedDecision = "NO OK";
//                                } else
//                                {
//                                    BadAnswer = true;
//                                }
//                            } else if (answer.ToString() == "D5FE6059-256D-46DD-A03A-533876191B96")
//                            {
//                                string clasification = row[ColumnIndexArray[i]].ToString();
//                                switch (clasification)
//                                {
//                                    case "1":
//                                        ToSaveAnswer = "CAFE EXCEPCIONAL";
//                                        SavedClasification = "CAFE EXCEPCIONAL";
//                                        break;
//                                    case "2":
//                                        ToSaveAnswer = "CAFE PERFIL NN";
//                                        SavedClasification = "CAFE PERFIL NN";
//                                        break;
//                                    case "3":
//                                        ToSaveAnswer = "BORDERLINE";
//                                        SavedClasification = "BORDERLINE";
//                                        break;
//                                    case "4":
//                                        ToSaveAnswer = "CAFE SIN PERFIL";
//                                        SavedClasification = "CAFE SIN PERFIL";
//                                        break;
//                                    case "5":
//                                        ToSaveAnswer = "CAFE CON DEFECTO";
//                                        SavedClasification = "CAFE CON DEFECTO";
//                                        break;
//                                    default:
//                                        BadAnswer = true;
//                                        break;
//                                }
//                            } else if (answer.ToString() == "90A8ECC4-14A5-4CA8-8B02-354A7F85341A")
//                            {
//                                SavedDefect = ToSaveAnswer;
//                            } else if (answer.ToString() == "E75FF320-9F01-4197-B139-D5F147C401CA")
//                            {
//                                SavedId = ToSaveAnswer;
//                            } else if (answer.ToString() == "71F45B44-692E-4487-8C93-3ACB7C9680F0")
//                            {
//                                SavedObservation = ToSaveAnswer;
//                            }
//                                assessment.SensoryProfileAnswers.Add(new SensoryProfileAnswerDTO
//                            {
//                                Answer = ToSaveAnswer,
//                                QualityAttributeId = new Guid(answer.ToString()),
//                                SensoryProfileAssessmentId = assessment.Id
//                            });
//                        };
//                        i++;
//                    }
//                    if (BadAnswer == true)
//                    {
//                        loadResults.Add(new List<object> { "BadAnswer", row[5].ToString() });
//                    } else
//                    {
//                        _sensoryProfileManager.Add(assessment);
//                        loadResults.Add(new List<object> { "AddedFarms", row[5].ToString(), SavedId, SavedClasification, SavedDecision, SavedDefect, SavedObservation });
//                    }
                    
//                }
//            }
//            else
//            {
//                List<string> SavedAnswers = new List<string>();
//                loadResults.Add(new List<object> { "BadFarms", row[5].ToString() });
//            }
//        }
//    }
//}