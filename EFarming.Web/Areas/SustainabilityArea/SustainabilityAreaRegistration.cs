using System.Web.Mvc;

namespace EFarming.Web.Areas.SustainabilityArea
{
    public class SustainabilityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SustainabilityArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SustainabilityArea_default",
                "SustainabilityArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}