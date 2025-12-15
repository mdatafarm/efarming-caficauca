using System.Web.Mvc;

namespace EFarming.Web.Areas.Comercial
{
    /// <summary>
    /// Comertial registration AREA
    /// </summary>
    public class ComercialAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "Comercial";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Comercial_default",
                "Comercial/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}