using Elmah;
using System.Web.Mvc;

namespace EFarming.Common.Filters
{
    /// <summary>
    /// ElmahHandledErrorLoggerFilter 
    /// </summary>
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}