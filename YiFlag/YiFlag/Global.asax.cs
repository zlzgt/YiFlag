using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace YiFlag
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        /// <summary>
        /// 只要web中出现了异常最终没有处理，都会进入这里
        /// </summary>
        public void Application_Error(object sender ,EventArgs e)
        {
            Exception error = Server.GetLastError();
            Console.Write(error.Message);
            Response.Write("Error");
            Response.ContentType = "text/html";
            Server.ClearError();
        }
    }
}
