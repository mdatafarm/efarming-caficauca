using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Web.Helpers
{
    public class PagerHelper
    {
        public static string IsActive(int current, int page)
        {
            var active = current == page;
            return active ? "active" : string.Empty;
        }
    }
}