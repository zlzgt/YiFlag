using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.Manage.Models;
using YiFlag.Model;
using YiFlag.Tools;

namespace YiFlag.Manage.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(MenuModel msg)
        {
            ReplyModel replyModel = new ReplyModel();
            if (ModelState.IsValid)
            {
                using(YiFlagContext dbContext=new YiFlagContext())
                {
                    SysMenue sysMenue = new SysMenue();
                    ModelHelper.CopyModel<SysMenue, MenuModel>(sysMenue,msg);
                    dbContext.Set<SysMenue>().Add(sysMenue);
                    int iResult=dbContext.SaveChanges();
                    if(iResult>0)
                    {
                        replyModel.code = 0;
                        replyModel.msg = "添加菜单成功";
                    }
                    else
                    {
                        replyModel.msg = "添加菜单失败，请重试";
                    }
                }
            }
            else
            {
                string errorInfo = ModelErrorInfo.GetErrorInfo(ModelState.Values);
                replyModel.msg = errorInfo;
            }
            return Json(replyModel, JsonRequestBehavior.AllowGet);
        }

    }
}