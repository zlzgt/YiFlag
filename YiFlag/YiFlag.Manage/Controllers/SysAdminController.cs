using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using YiFlag.Manage.Models;
using YiFlag.Model;
using YiFlag.Tools;

namespace YiFlag.Manage.Controllers
{
    public class SysAdminController : Controller
    {
        // GET: SysAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                List<SysRoles> sysRoles = dbContext.Set<SysRoles>().Where(w => w.Id != 1).ToList();
                return View(sysRoles);
            }
        }
        #region 添加网站后台管理员
        [HttpPost]
        public JsonResult AddResult(SysAdminInfo info)
        {
            ReplyModel reply = new ReplyModel();
            DbContextTransaction transaction = null;
            try
            {
                if (ModelState.IsValid)
                {
                    using (YiFlagContext dbContext = new YiFlagContext())
                    {
                        using (transaction = dbContext.Database.BeginTransaction())
                        {
                            SysAdmin sysAdmin = new SysAdmin();
                            sysAdmin.UserName = info.UserName;
                            sysAdmin.PassWord = MD5Encrypt.Encrypt(info.PassWord);
                            sysAdmin.State = 1;
                            sysAdmin.Remark = info.Remark;
                            sysAdmin.CreateTime = DateTime.Now;
                            dbContext.Set<SysAdmin>().Add(sysAdmin);
                            dbContext.SaveChanges();

                            if (info.RolesId > 0)
                            {
                                SysAdminRoles roles = new SysAdminRoles();
                                roles.UserId = sysAdmin.Id;
                                roles.RolesId = info.RolesId;
                                roles.CreateTime = DateTime.Now;
                                dbContext.Set<SysAdminRoles>().Add(roles);
                                dbContext.SaveChanges();
                            }
                            transaction.Commit();
                            reply.code = 0;
                            reply.msg = "添加用户成功";
                        }
                    }
                }
                else
                {
                    string errorInfo = ModelErrorInfo.GetErrorInfo(ModelState.Values);
                    reply.msg = errorInfo;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                reply.msg = $"出现异常，请联系管理员{ex.Message}";
            }
            return Json(reply, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取后台用户列表
        public ActionResult AdminList(PageModle msg)
        {
            ReplyModel reply = new ReplyModel();
            if (ModelState.IsValid)
            {
                using (YiFlagContext dbContext = new YiFlagContext())
                {
                    List<SysAdmin> sysAdmins = dbContext.Set<SysAdmin>().Where(w => w.State == 1).OrderBy(w => w.Id).Skip((msg.page - 1) * msg.limit).Take(msg.limit).ToList();
                    reply.count = dbContext.Set<SysAdmin>().Where(w => w.State == 1).Count();
                    if (sysAdmins != null)
                    {

                        var userRoles = dbContext.SysRoles.Join(dbContext.SysAdminRoles, a => a.Id, g => g.RolesId, (a, g) => new { a.Id, a.RolesName, g.UserId }).ToList();
                        List<RSysAdminInfo> adminInfos = new List<RSysAdminInfo>();
                        foreach(var item in sysAdmins)
                        {
                            RSysAdminInfo info = new RSysAdminInfo();
                            info.CreateTime = item.CreateTime;
                            info.Id = item.Id;
                            info.Remark = item.Remark;
                            info.State = item.State;
                            info.UserName = item.UserName;
                            var user = userRoles.Where(w => w.UserId == item.Id).FirstOrDefault();
                            if(user!=null)
                            {
                                info.RolesId = user.Id;
                                info.RolesName =user.RolesName;
                            }
                            adminInfos.Add(info);
                        }
                        reply.code = 0;
                        reply.msg = "获取数据成功";
                        reply.data = adminInfos;
                    }
                    else
                    {
                        reply.msg = "没有数据";
                    }
                }
            }
            else
            {
                string errorInfo = ModelErrorInfo.GetErrorInfo(ModelState.Values);
                reply.msg = errorInfo;
            }
            return Json(reply, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}