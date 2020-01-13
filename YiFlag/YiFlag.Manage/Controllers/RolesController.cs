using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.Model;
using YiFlag.Tools;

namespace YiFlag.Manage.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRoles(PageModle msg)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                var list = dbContext.Set<SysRoles>().AsQueryable();
                if (!string.IsNullOrEmpty(msg.keyword))
                    list = list.Where(t => t.RolesName.Contains(msg.keyword)); //根据名称查找数据
                int count = list.Select(t => t.Id).Count();
                List<SysRoles> roles = list.OrderBy(t => t.Id).Skip((msg.page - 1) * msg.limit).Take(msg.limit).ToList();
                List<RolesModle> rolesList = new List<RolesModle>();
                if (roles != null)
                {
                    foreach (SysRoles l in roles)
                    {
                        RolesModle rolesModle = new RolesModle();
                        rolesModle.Id = l.Id;
                        rolesModle.RolesName = l.RolesName;
                        rolesModle.Remark = l.Remark;
                        rolesModle.CreateTime = l.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        rolesList.Add(rolesModle);
                    }
                }
                return Json(new { code = 0, count, data = rolesList, msg = "获取数据成功" }, JsonRequestBehavior.AllowGet);
            }
        }


        #region 获取菜单树
        public JsonResult GetMenueTree()
        {
            List<TreeChildViewModel> treeViewModels = new List<TreeChildViewModel>();
            MenueTree mune = new MenueTree();
            treeViewModels = mune.AddChildN(0);
            string s = JsonConvert.SerializeObject(treeViewModels);
            return Json(new { code = 1, data = treeViewModels, msg = "获取数据成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取角色菜单
        public JsonResult GetRolesMenue(int Id)
        {
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                List<int> menueId = new List<int>();
                MenueTree menue = new MenueTree();
                if (Id == 1)
                {
                    menueId = dbContext.Set<SysMenue>().Select(w => w.Id).ToList<int>();
                }
                else
                {
                    var menues = dbContext.Set<SysRolesMenueFuction>().Where(w => w.RolesId == Id).Select(w => w.MenuId).OrderBy(w => w).ToList<int>();
                    foreach (var item in menues)
                    {
                        if (!menue.IsHasChild(item))
                        {
                            menueId.Add(item);
                        }
                    }
                }
                return Json(new { state = 1, msg = "获取菜单成功", data = menueId }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region  设置角色权限
        public JsonResult SetRolesAuthority(string s, int rolesId)
        {
            DbContextTransaction transaction = null;
            try
            {
                List<int> iResultList = new List<int>();
                JsonReader reader = new JsonTextReader(new StringReader(s));
                while (reader.Read())
                {
                    if (reader.TokenType.ToString() == "Integer")
                    {
                        int i = Convert.ToInt32(reader.Value);
                        iResultList.Add(i);
                    }
                }
                using (YiFlagContext dbContext = new YiFlagContext())
                {
                    using (transaction = dbContext.Database.BeginTransaction())
                    {
                        List<SysRolesMenueFuction> sysRolesMenueList = new List<SysRolesMenueFuction>();
                        var sysRolesMenueFuction = dbContext.Set<SysRolesMenueFuction>().Where(w => w.RolesId == rolesId).ToList();
                        if (sysRolesMenueFuction != null)
                        {
                            dbContext.Set<SysRolesMenueFuction>().RemoveRange(sysRolesMenueFuction);
                            dbContext.SaveChanges();
                        }

                        foreach (int item in iResultList)
                        {
                            SysRolesMenueFuction sysRolesMenue = new SysRolesMenueFuction();
                            sysRolesMenue.MenuId = item;
                            sysRolesMenue.RolesId = rolesId;
                            sysRolesMenue.CreateTime = DateTime.Now;
                            sysRolesMenueList.Add(sysRolesMenue);
                        }
                        dbContext.Set<SysRolesMenueFuction>().AddRange(sysRolesMenueList);
                        int iResult = dbContext.SaveChanges();
                        if (iResult > 0)
                        {
                            transaction.Commit();
                            return Json(new { state = 1, msg = "角色权限菜单配置成功" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { state = -1, msg = "角色权限菜单配置失败" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                LogHelper.WriteLog($"角色权限菜单配置出现异常{ex.Message}", Log4NetLevel.Error);
                return Json(new { state = -1, msg = "角色权限菜单配置出现异常" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


    }
}