//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Twilio;
//using Twilio.Pricing;

//namespace EFarming.Web.Areas.Admin.Controllers
//{
//    /// <summary>
//    /// Controlador Funcionalidades para enviar mensaje de texto
//    /// </summary>
//    public class SMSEfarmingController : Controller
//    {

//        // Configuration for Twilio
//        // Find your Account Sid and Auth Token at twilio.com/user/account 
//        string AccountSid = ConfigurationManager.AppSettings["AccountSid"].ToString();
//        // Token de la aplicacion
//        string AuthToken = ConfigurationManager.AppSettings["AuthToken"].ToString();
//        // El destinatario es la linea que asignan en la api y si no se coloca esta no funciona la API
//        string destinatario = ConfigurationManager.AppSettings["destinatario"].ToString();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index(){ return View(); }

//        /// <summary>
//        /// Sends the SMS.
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult SendSMS()
//        {
//	  return View(new Twilio.Message());
//        }

//        /// <summary>
//        /// Configurations this instance.
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult Configuration()
//        {
//	  return View();
//        }

//        /// <summary>
//        /// Abouts this instance.
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult About()
//        {
//	  return View();
//        }

//        /// <summary>
//        /// Sends the message.
//        /// </summary>
//        /// <param name="msg">The MSG.</param>
//        /// <returns>to the view GetMessages</returns>
//        [HttpPost]
//        public ActionResult SendMessage(Message msg)
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  var messageSended = twilio.SendMessage(destinatario, msg.To, msg.Body);	  

//	  // documentacion de los errores en twilio
//	  // https://www.twilio.com/docs/api/rest/message#error-values
//	  return View("GetMessages");
//        }


//        /// <summary>
//        /// Gets the messages.
//        /// </summary>
//        /// <returns>ListMessageResponse</returns>
//        public ActionResult GetMessages()
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  // Build the parameters 
//	  var options = new MessageListRequest();
//	  var ListMessagesResponse = twilio.ListMessages(options);
//	  return View(ListMessagesResponse);
//        }

//        /// <summary>
//        /// This action return the information about one sended Message
//        /// </summary>
//        /// <param name="MessageSid">id of Message</param>        
//        public ActionResult GetMessageById(string MessageSid)
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  var message = twilio.GetMessage(MessageSid);
//	  ViewBag.temp = message;
//	  return View();
//        }

//        /// <summary>
//        /// Get the States the account and transactions in twilio.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult StateAccount()
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  Twilio.UsageResult list = twilio.ListUsage(null,null, null, null, null, null);
//	  return View(list);
//        }

//        /// <summary>
//        /// Addresseses this instance.
//        /// </summary>
//        /// <returns>The view</returns>
//        public ActionResult Addresses()
//        {
//	  return View();
//        }

//        /// <summary>
//        /// Adds the address.
//        /// </summary>
//        /// <param name="_address">The _address.</param>
//        /// <returns>The view List Address</returns>
//        [HttpPost]
//        public ActionResult addAddress(Twilio.Address _address)
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);

//	  try{
//	      var addResult = twilio.AddAddress(_address.FriendlyName, _address.CustomerName, _address.Street, _address.City, _address.Region, _address.PostalCode, _address.IsoCountry);
//	      return View("ListAddress");
//	  }
//	  catch { return View(); }
//        }

//        /// <summary>
//        /// Lists the address.
//        /// </summary>
//        /// <returns>list of address</returns>
//        public ActionResult ListAddress()
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  var options = new AddressListRequest();
//	  var listAddress = twilio.ListAddresses(options);
//	  return View(listAddress);
//        }

//        /// <summary>
//        /// Edits the specified identifier customer.
//        /// </summary>
//        /// <param name="idCustomer">The identifier customer.</param>
//        /// <returns>the view edit with address</returns>
//        public ActionResult Edit(string idCustomer)
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  var Address = twilio.GetAddress(idCustomer);
//	  return View(Address);
//        }

//        /// <summary>
//        /// Edits the address.
//        /// </summary>
//        /// <param name="_address">The _address.</param>
//        /// <returns>Redirect to ListAddress</returns>
//        [HttpPost]
//        public ActionResult EditAddress(Twilio.Address _address)
//        {
//	  var twilio = new TwilioRestClient(AccountSid, AuthToken);
//	  var options = new AddressOptions();

//	  options.City = _address.City;
//	  options.CustomerName = _address.CustomerName;
//	  options.FriendlyName = _address.FriendlyName;
//	  options.PostalCode = _address.PostalCode;
//	  options.Region = _address.Region;
//	  options.Street = _address.Street;

//	  var response = twilio.UpdateAddress(_address.Sid, options);

//	  return RedirectToAction("ListAddress");
//        }
//    }
//}