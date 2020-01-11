using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YiFlag.Model
{
   public class MyExceptionFilterAtrribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            HttpContext.Current.Response.Redirect("/Error/Index");
        }
    }
}
