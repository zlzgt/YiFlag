using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.BLL;
using YiFlag.IBLL;
using YiFlag.Model;
using YiFlag.Tools;
using Unity;
namespace YiFlag.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public string GetBlogList(Page page)
        {
            page.pageSize = 10;
            IBlogManage blogManage = UnityFactory.CreateInstance().Resolve<IBlogManage>();
            return blogManage.GetBlogList(page);
        }
    }
}