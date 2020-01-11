
using System.Web;
using System.Web.Mvc;
using YiFlag.Model;

namespace YiFlag
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
