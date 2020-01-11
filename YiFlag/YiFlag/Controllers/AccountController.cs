using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.BLL;
using YiFlag.IBLL;
using YiFlag.Model;
using Unity;
using YiFlag.Tools;
using System.Text;
using YiFlag.Models;

namespace YiFlag.Controllers
{
    public class AccountController : Controller
    {
        private IUserManage userManage= UnityFactory.CreateInstance().Resolve<IUserManage>();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(SysUser user)
        {
            if (ModelState.IsValid)
            {
                SysUser sysUser = userManage.Register(user);
                if(sysUser!=null)
                {
                    return Content("<script>alert('注册成功'); window.location.href = '../Login/Index';</script>");
                }
                else
                {
                    return Content("<script>alert('注册失败,账号已存在');history.go(-1);</script>");
                }
            }
            else
            {
                return Content("<script>alert('账号或密码不能为空')</script>");
            }
        }
        public ActionResult Login(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                SysUser u= userManage.Login(user);
                if (u!=null)
                {
                    Session[CurrentManage.SESSIONNAME] =u;
                    return Content("<script>alert('登录成功');window.location.href = '../Home/Index';</script>");
                }
                else
                {
                    return Content("<script>alert('登录失败');history.go(-1);</script>");
                }
            }
            else
            {
                string errInfo=ModelErrorInfo.GetErrorInfo(ModelState.Values);
                return Content("<script>alert('"+ errInfo + "');history.go(-1);</script>");
            }
        }
    }
}