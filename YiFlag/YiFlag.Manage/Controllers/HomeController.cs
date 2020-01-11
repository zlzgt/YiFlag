using AuthorityYiFlag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YiFlag.Manage.Controllers
{
    public class HomeController : Controller
    {
        Sys_MenuLogic _Sys_MenuLogic = new Sys_MenuLogic();
        // GET: Home
        public ActionResult Index()
        {
            StringBuilder _StringBuilder = new StringBuilder();
            _Sys_MenuLogic.CreateMenus(0, _StringBuilder);
            ViewData["MenuHtml"] = HttpUtility.HtmlDecode(_StringBuilder.ToString());
            return View();
        }
        public ActionResult Console()
        {
            return View();
        }
    }
}