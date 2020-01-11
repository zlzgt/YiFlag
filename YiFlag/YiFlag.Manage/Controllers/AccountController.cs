using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YiFlag.IBLL;
using YiFlag.Model;
using YiFlag.Tools;
using Unity;
using YiFlag.Manage.Models;

namespace YiFlag.Manage.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region 后台登录首页
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 后台用户登录
        public JsonResult Login(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                string logincode = CookieHelper.GetCookie("loginCode");
                if (string.IsNullOrEmpty(logincode))
                {
                    return Json(new { code = -1, msg = "验证码失效" }, JsonRequestBehavior.AllowGet);
                }
                if (user.VerificationCode.ToLower() == logincode)
                {
                    ISysAdminManage sysAdminMange = UnityFactory.CreateInstance().Resolve<ISysAdminManage>();
                    SysAdmin u = sysAdminMange.Login(user);
                    if (u != null)
                    {
                        Session[CurrentManage.SESSIONNAME] = u;
                        return Json(new { code = 0, msg = "登录成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { code = -1, msg = "账号或密码错误" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { code = -1, msg = "验证码错误" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                string errorInfo=ModelErrorInfo.GetErrorInfo(ModelState.Values);
                return Json(new { code = -1, msg = errorInfo }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 获取验证码
        public ActionResult GetYZM()
        {
            ValidateCodeHelper vch = new ValidateCodeHelper();
            string code = vch.GetRandomNumberString(4).ToLower();
            CookieHelper.SetCookie("loginCode", code, 2);
            return File(vch.CreateImage(code), "image/jpeg");
        }
        #endregion

    }
}