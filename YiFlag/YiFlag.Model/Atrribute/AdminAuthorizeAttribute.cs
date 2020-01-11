using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YiFlag.Model
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userSession = System.Web.HttpContext.Current.Session[CurrentManage.SESSIONNAME];

            if (userSession != null)
            {
                SysAdmin user = userSession as SysAdmin;
                if (user != null && user.Id > 0)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {

                    context.HttpContext.Response.StatusCode = 202;//这个可以指定为其他的
                    return;

                }
                else
                {
                    string path = HttpUtility.UrlEncode(context.HttpContext.Request.RawUrl);
                    string strUrl = "/Account/Index";
                    //这才是正解，使用下面方法依然还会执行
                    context.Result = new ContentResult { Content = "<script>top.location.href='" + strUrl + "?redirectUrl=" + path + "'</script>" };
                }
            }
        }
    }
}
