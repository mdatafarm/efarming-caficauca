using EFarming.Web.Dependency;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor.Installer;
using System.Web.Security;
using EFarming.DTO.AdminModule;
using EFarming.Web.Models;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Common.Encription;
using EFarming.DAL;
using EFarming.Web.Util;
using System.Data.Entity.SqlServer;

namespace EFarming.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Global ASAX
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The container
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcApplication"/> class.
        /// </summary>
        public MvcApplication()
        {
            this.container =
                new WindsorContainer().Install(new DependencyConventions());
        }

        /// <summary>
        /// Disposes the <see cref="T:System.Web.HttpApplication" /> instance.
        /// </summary>
        public override void Dispose()
        {
            this.container.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            //Start
            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            //SqlProviderServices.SqlServerTypesAssemblyName ="Microsoft.SqlServer.Types, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
            //End
            AjaxHelper.GlobalizationScriptPath = "http://ajax.microsoft.com/ajax/4.0/1/globalization/";
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobScheduler.Start();
            //JobScheduler.UpdateProductivity();
            AuthConfig.RegisterAuth();
            GlobalConfiguration.Configuration.EnsureInitialized();

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }

        public class UserToCookie
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<string> Roles { get; set; }
        }

        /// <summary>
        /// Handles the PostAuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                UserToCookie serializeModel = JsonConvert.DeserializeObject<UserToCookie>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.Id;
                newUser.FirstName = serializeModel.FirstName;
                newUser.LastName = serializeModel.LastName;
                newUser.Roles = serializeModel.Roles;

                HttpContext.Current.User = newUser;
            }
        }
    }
}