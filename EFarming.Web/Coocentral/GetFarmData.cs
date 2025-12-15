using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.DAL;
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
    public class GetFarmData
    {
        private UnitOfWork db = new UnitOfWork();
        public async Task<string> GetFarmInformation(IFarmManager _farmmanager, List<FarmDTO> Farms)
        {
            string answer = null;
            //Conection with the Oracle API.
            //Saving the JSON in an Farms object list
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["OracleAPI"] + "farms");
            try
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                if (result != "\"doesn't exist data\"")
                {
                    List<Farm> items = JsonConvert.DeserializeObject<List<Farm>>(result);

                    //Loop for each farm in the list
                    foreach (var farm in items)
                    {
                        FarmDTO FarmToSAve = new FarmDTO();
                        FamilyUnitMemberDTO Owner = new FamilyUnitMemberDTO();

                        FarmToSAve = CreateFarm(farm, FarmToSAve);
                        Owner = CreateOwner(farm, Owner);

                        //Farm verified, if the farm exists, the information is updated
                        var FarmExists = _farmmanager.GetFarmByCode(FarmToSAve.Code);
                        if (FarmExists.Code != null)
                        {
                            FarmToSAve.Id = FarmExists.Id;
                            var Updated = _farmmanager.Edit(FarmExists.Id, FarmToSAve, FarmManager.FARMS);

                            //if the farm was updated,then the actual owner is deleted and the new owner is added
                            if (Updated == true)
                            {
                                FarmToSAve = _farmmanager.Details(FarmExists.Id);
                                Owner.FarmId = FarmExists.Id;
                                var FamilyMembers = FarmToSAve.FamilyUnitMembers.ToList();

                                //Reading the family memebers and deleting the actual Owner
                                foreach (var familyMember in FamilyMembers)
                                {
                                    if (familyMember.Identification == Owner.Identification)
                                    {
                                        FarmToSAve.FamilyUnitMembers.Remove(familyMember);
                                    }
                                }
                                //Adding the Owner to the farm
                                FarmToSAve.FamilyUnitMembers.Add(Owner);
                                _farmmanager.Edit(FarmExists.Id, FarmToSAve, FarmManager.FAMILY_UNIT_MEMBERS);
                            }
                            //Creating a new Farm
                        }
                        else
                        {
                            var Saved = _farmmanager.Create(FarmToSAve);
                            var FarmSaved = _farmmanager.GetFarmByCode(FarmToSAve.Code);

                            //If the Farm is saved, the Owner is created
                            if (FarmSaved.Code != null)
                            {
                                Owner.FarmId = Saved.Id;
                                Saved.FamilyUnitMembers.Add(Owner);
                                _farmmanager.Edit(Saved.Id, Saved, FarmManager.FAMILY_UNIT_MEMBERS);
                            }
                        }
                        Farms.Add(FarmToSAve);

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

        //Converts the farm information in a FarmDTO object
        private FarmDTO CreateFarm(Farm farm, FarmDTO FarmToSAve)
        {
            Village village = new Village();
            var Municipality = db.Municipalities.Where(m => m.Code == farm.MunicipalityId).First();
            try
            {
                village = db.Villages.Where(v => v.Name.Equals(farm.Village) && v.MunicipalityId == Municipality.Id).FirstOrDefault();
                if(village == null)
                {
                    Village newVillage = new Village();
                    newVillage.Id = Guid.NewGuid();
                    newVillage.MunicipalityId = Municipality.Id;
                    newVillage.Name = farm.Village;
                    newVillage.CreatedAt = DateTime.Now;
                    db.Villages.Add(newVillage);
                    db.SaveChanges();
                    village = db.Villages.Where(v => v.Name.Equals(farm.Village) && v.MunicipalityId == Municipality.Id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var error = e;
            }

            FarmToSAve.Code = farm.Code.ToString();
            FarmToSAve.Name = farm.Name;
            if (farm.Name == "")
            {
                FarmToSAve.Name = farm.FarmerName;
            }
            FarmToSAve.VillageId = village.Id;
            FarmToSAve.CooperativeId = new Guid("{C1D43298-C547-476C-A483-17361EEC98ED}");
            FarmToSAve.FarmStatusId = new Guid("{D0594E3A-AB74-441C-9E38-1E3C3EFAD394}");
            FarmToSAve.SupplyChainId = new Guid("{C8BE0458-39C5-40A2-A3AF-31387FD3D14C}");
            FarmToSAve.OwnershipTypeId = new Guid("{C4192128-0B70-402B-9EA9-49BD2C18B6E5}");

            return FarmToSAve;
        }

        //Converts the Owner information in a FamilyUnitMember object
        private FamilyUnitMemberDTO CreateOwner(Farm farm, FamilyUnitMemberDTO Owner)
        {
            Owner.Identification = farm.Code.ToString();
            Owner.FirstName = farm.FarmerName;
            Owner.LastName = farm.LastName;
            Owner.MaritalStatus = farm.CivilStatus;
            Owner.IsOwner = true;

            //The date of birthday is verified, if this date has a wrong format is replaced by the actual date
            try
            {
                Owner.Age = Convert.ToDateTime(farm.DateOfBirthday);
            }
            catch (Exception DateOfBirthdayException)
            {
                var DateOfBirthdayError = DateOfBirthdayException;
                Owner.Age = DateTime.Now;
            }

            return Owner;
        }
    }
}