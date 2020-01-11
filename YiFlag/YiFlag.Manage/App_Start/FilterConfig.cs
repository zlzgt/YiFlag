
using System.Web;
using System.Web.Mvc;
using YiFlag.Model;

namespace YiFlag.Manage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new AdminAuthorizeAttribute ());
        }
    }
}
