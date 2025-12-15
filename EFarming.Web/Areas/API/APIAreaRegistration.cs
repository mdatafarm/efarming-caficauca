using System.Web.Mvc;
using System.Web.Http;

namespace EFarming.Web.Areas.API
{
    /// <summary>
    /// API Area Registration
    /// </summary>
    public class APIAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "API";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            /*The order of configuration of route its important */
            context.Routes.MapHttpRoute(
                name: "DashboardApi",
                routeTemplate: "api/dashboard/{action}",
                defaults: new { controller = "Dashboard" }
            );

            context.Routes.MapHttpRoute(
                name: "ContractsApi",
                routeTemplate: "api/commercial/{action}",
                defaults: new { controller = "Commercial" }
            );

            context.Routes.MapHttpRoute(
                name: "QualitydApi",
                routeTemplate: "api/qualitydashboard/{action}",
                defaults: new { controller = "QualityDashboard" }
            );

            context.Routes.MapHttpRoute(
                name: "SustainabilityApi",
                routeTemplate: "api/sustainabilitydashboard/{action}",
                defaults: new { controller = "SustainabilityDashboard" }
            );

            context.Routes.MapHttpRoute(
                name: "ComertialApi",
                routeTemplate: "api/ComertialDashboard/{action}",
                defaults: new { controller = "ComertialDashboard" }
            );

            context.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}