using System.Web;
using System.Web.Mvc;

namespace SENAI.ExemploStartUp.UI.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
