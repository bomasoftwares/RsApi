using Boma.Rs.Api.StartupConfigurations.Filters;
using System.Web;
using System.Web.Mvc;

namespace Boma.Rs.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
