using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YiFlag.Manage.Models;
using YiFlag.Model;
using YiFlag.Tools;

namespace YiFlag.Manage.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public JsonResult UserList(PageModle msg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (YiFlagContext dbContext = new YiFlagContext())
                    {
                        var list = dbContext.Set<SysUser>().AsQueryable();
                        if (!string.IsNullOrEmpty(msg.keyword))
                            list = list.Where(t => t.UserName.Contains(msg.keyword)); //根据名称查找数据
                        int count = list.Select(t => t.Id).Count();
                        var data = list.OrderBy(t => t.Id).Skip((msg.page - 1) * msg.limit).Take(msg.limit).ToList();
                        return Json(new { code = 0, count, data, msg = "获取数据成功" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string errorInfo = ModelErrorInfo.GetErrorInfo(ModelState.Values);
                    return Json(new { code = -1, msg = errorInfo }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}",Log4NetLevel.Error);
                return Json(new { code = -1, msg ="出现异常" },JsonRequestBehavior.AllowGet);  
            }
        }


        public JsonResult Update(int Id ,string field,string value)
        {
            using(YiFlagContext dbContext=new YiFlagContext())
            {
               SysUser user=dbContext.Set<SysUser>().Where(w => w.Id == Id).FirstOrDefault();
               if("UserName"==field)
                {
                    user.UserName = value;
                }
                dbContext.SaveChanges();

            }
            return Json(new { code = 0,msg="修改成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public JsonResult AddResult(SysUser user)
        {
            try
            {
                using(YiFlagContext dbContext=new YiFlagContext())
                {
                    user.Pwd = MD5Encrypt.Encrypt(user.Pwd);
                    user.State = 1;
                    user.AddTime = DateTime.Now;
                    dbContext.Set<SysUser>().Add(user);
                    int i= dbContext.SaveChanges();
                    if(i>0)
                    {
                        return Json(new {state=1,data=user,msg="添加成功"},JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { state = 0, msg = "添加失败" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}",Log4NetLevel.Error);
                return Json("", JsonRequestBehavior.AllowGet);
            }
          
        }

        public JsonResult Delete(List<SysUser> userList)
        {
            try
            {
                using (YiFlagContext dbContext = new YiFlagContext())
                {
                    List<int> sysUserIds = userList.Select(w => w.Id).ToList();
                    var sysUsers = dbContext.Set<SysUser>().Where(w => sysUserIds.Contains(w.Id)).ToList();
                    foreach (var user in sysUsers)
                    {
                        user.State = 0;
                    }
                    int i = dbContext.SaveChanges();
                    if (i > 0)
                    {
                        return Json(new { state = 1, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { state = -1, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}",Log4NetLevel.Error);
                return Json(new { state = -1, msg = "出现异常" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}