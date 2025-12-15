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
    public class GetFamilyData
    {
        public async Task<string> GetFamilyInformation(IFarmManager _farmmanager)
        {
            string answer = null;
            //Conection with the Oracle API.
            //Saving the JSON in an FamilyMember object list
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["OracleAPI"] + "familymembers");
            try
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                if (result != "\"doesn't exist data\"")
                {
                    List<FamilyMember> items = JsonConvert.DeserializeObject<List<FamilyMember>>(result);

                    //Loop for each family member in the list
                    foreach (var familymember in items)
                    {
                        FamilyUnitMemberDTO familymemberToAdd = new FamilyUnitMemberDTO();
                        familymemberToAdd = CreateFamilyMember(familymember, familymemberToAdd);

                        //Farm verified, if the farm exists, the information is updated
                        var FarmExists = _farmmanager.GetFarmByCode(familymember.FarmerIdentification.ToString());
                        if (FarmExists.Code != null)
                        {
                            familymemberToAdd.FarmId = FarmExists.Id;
                            //Reading the family memebers and deleting the actual people
                            foreach (var familyMember in FarmExists.FamilyUnitMembers.ToList())
                            {
                                if (familyMember.Identification == familymemberToAdd.Identification.ToString())
                                {
                                    FarmExists.FamilyUnitMembers.Remove(familyMember);
                                }
                            }
                            FarmExists.FamilyUnitMembers.Add(familymemberToAdd);
                            //Adding the people to the farm
                            _farmmanager.Edit(FarmExists.Id, FarmExists, FarmManager.FAMILY_UNIT_MEMBERS);
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

        private FamilyUnitMemberDTO CreateFamilyMember(FamilyMember familymember, FamilyUnitMemberDTO familymemberToAdd)
        {
            familymemberToAdd.Identification = familymember.Identification.ToString();
            familymemberToAdd.FirstName = familymember.Names;
            familymemberToAdd.LastName = familymember.LastNames;
            familymemberToAdd.MaritalStatus = familymember.CivilStatus;
            familymemberToAdd.Relationship = familymember.FarmerRelationship;
            familymemberToAdd.IsOwner = false;

            //The date of birthday is verified, if this date has a wrong format is replaced by the actual date
            try
            {
                familymemberToAdd.Age = Convert.ToDateTime(familymember.dob);
            }
            catch (Exception DateOfBirthdayException)
            {
                var DateOfBirthdayError = DateOfBirthdayException;
                familymemberToAdd.Age = DateTime.Now;
            }

            return familymemberToAdd;
        }
    }
}