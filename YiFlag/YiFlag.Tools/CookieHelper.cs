using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YiFlag.Tools
{
    public class CookieHelper
    {
        #region Cookie操作
        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时长</param>
        public static void SetCookie(string key, string value, int? expires = null)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
                cookie = new HttpCookie(key);
            cookie.Value = value;
            if (expires != null)
                cookie.Expires = DateTime.Now.AddMinutes(Convert.ToDouble(expires));
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读Cookie值
        /// </summary>
        /// <param name="strName">cookies名称</param>
        /// <returns>返回的cookies</returns>
        public static string GetCookie(string key)
        {
            var obj = HttpContext.Current.Request.Cookies[key];
            if (obj == null) return null;
            return obj.Value;
        }

        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public static void RemoveCookie(string key)
        {
            HttpContext.Current.Response.Cookies.Remove(key);
        }
        #endregion Cookie操作
    }
}
