using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YiFlag.Model
{
   public class AdminManage
    {
        public const string SESSIONNAME = "_current_manage"; //Session 名
        static bool isLogin = false;// 是否登录

        public const string BackstageWebsite = "yiFlag后台管理系统";   //后台网站名

        public static bool IsLogin
        {
            get
            {
                var current = HttpContext.Current.Session[SESSIONNAME];
                if (current != null)
                {
                    SysAdmin user = current as SysAdmin;
                    if (user.UserName != "")
                        return true;
                }
                return isLogin;
            }
        }

        static string userName = "登录"; //用户名

        public static string UserName
        {
            get
            {
                var current = HttpContext.Current.Session[SESSIONNAME];
                if (current != null)
                {
                    SysAdmin user = current as SysAdmin;
                    if (user.UserName != "")
                        return user.UserName;
                }
                return userName;
            }
        }

        public static int Id //ID
        {
            get
            {
                var current = HttpContext.Current.Session[SESSIONNAME];
                if (current != null)
                {
                    SysAdmin user = current as SysAdmin;
                    return user.Id;
                }
                return 0;
            }
        }
    }
}
