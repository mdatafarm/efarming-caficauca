using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EFarming.Web.Helpers
{
    public class MenuHelper
    {
        public static string IsSubactive(RouteData routeData, string controller, string action)
        {
            var subactive = controller.Equals(routeData.Values["controller"]) && 
                action.Equals(routeData.Values["action"]);
            return subactive ? "sub-active" : string.Empty;
        }

        public static string IsSubactive(RouteData routeData, string controller)
        {
            var subactive = controller.Equals(routeData.Values["controller"]);
            return subactive ? "sub-active" : string.Empty;
        }

        public static string IsActive(RouteData routeData, string controller)
        {
            var active = controller.Equals(routeData.Values["controller"]);
            return active ? "active" : string.Empty;
        }

        public static string IsActive(RouteData routeData, params string[] controllers)
        {
            var active = controllers.Contains(routeData.Values["controller"]);
            return active ? "active" : string.Empty;
        }

        public static string IsActive(RouteData routeData, string controller, string action)
        {
            var active = controller.Equals(routeData.Values["controller"]) &&
                action.Equals(routeData.Values["action"]);
            return active ? "active" : string.Empty;
        }

        public static bool InController(RouteData routeData, string[] controllers)
        {
            return controllers.Contains(routeData.Values["controller"]);
        }

        public static bool InAction(RouteData routeData, string controller, string action)
        {
            return controller.Equals(routeData.Values["controller"]) && action.Equals(routeData.Values["action"]);
        }
    }
}