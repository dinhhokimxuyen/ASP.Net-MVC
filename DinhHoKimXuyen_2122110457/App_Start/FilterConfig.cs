using System.Web;
using System.Web.Mvc;

namespace DinhHoKimXuyen_2122110457
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
