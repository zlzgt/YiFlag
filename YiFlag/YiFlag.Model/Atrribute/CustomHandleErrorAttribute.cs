using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using YiFlag.Tools;

namespace YiFlag.Model
{
    public class CustomHandleErrorAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if(!filterContext.ExceptionHandled)
            {
                Console.WriteLine($"{DateTime.Now}{filterContext.HttpContext.Request.Url.ToString()}{filterContext.RouteData.Values["Controller"]}出现异常:{filterContext.Exception.Message}");
                LogHelper.WriteLog($"{DateTime.Now}{filterContext.HttpContext.Request.Url.ToString()}{filterContext.RouteData.Values["Controller"]}出现异常:{filterContext.Exception.Message}",Log4NetLevel.Error);
                if(filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                            Data = new { 
                            Msg = "出现异常,请联系管理员",
                            Result = false,
                        }
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData=new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true; // 表示异常异常已经被处理过了
            }
        }
    }
}
