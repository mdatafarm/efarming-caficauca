//using AutoMapper;
//using EFarming.Core.ComercialModule;
//using EFarming.DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace EFarming.Web.Areas.API.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class CommercialController : ApiController
//    {
//        private UnitOfWork db = new UnitOfWork();
//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public List<List<object>> Index(string ClientId)
//        {
//            List<Agent> Agents;
//            Agents = Mapper.Map<List<Agent>>(db.Agents.Where(c => c.ClientId.Equals(new Guid(ClientId))).ToList());

//            List<List<object>> AgentsData1 = new List<List<object>>();
//            foreach (Agent Agent in Agents)
//            {
//                AgentsData1.Add(new List<object> { Agent.Id, Agent.Name });
//            }
//            return AgentsData1;
//        }

//        /// <summary>
//        /// Mores the information.
//        /// </summary>
//        /// <param name="ClientId">The client identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public List<object> MoreInformation(string ClientId)
//        {
//            List<MoreInformation> MoreInformation;
//            MoreInformation = Mapper.Map<List<MoreInformation>>(db.Moreinformation
//                                                                    .Where(c => c.ClientId.Equals(new Guid(ClientId)))
//                                                                    .OrderBy(o => o.InformationType)
//                                                                    .ThenBy(o => o.Order).ToList());

//            List<object> MoreInformationData = new List<object>();
//            foreach (MoreInformation Item in MoreInformation)
//            {
//                MoreInformationData.Add( new { InformationType = Item.InformationType, Order = Item.Order, Text = Item.Text, Short = Item.Short } );
//            }
//            return MoreInformationData;
//        }
//    }
//}
