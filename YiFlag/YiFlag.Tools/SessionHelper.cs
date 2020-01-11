using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
namespace YiFlag.Tools
{
   public class SessionHelper
    {
        private static HttpSessionState _session = HttpContext.Current.Session;

        public static void SetSession(string key, object value)
        {
            _session[key] = value;
        }
        public static int GetSessionNumber(string key)
        {
            int result = 0;
            if (_session[key] != null)
            {
                int.TryParse(_session[key].ToString(), out result);
            }
            return result;
        }
        public static string GetSessionString(string key)
        {
            string result = "";
            if (_session[key] != null)
            {
                result = _session[key].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取 Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSession<T>(string key)
        {
            try
            {
                var Obj = (T)(HttpContext.Current.Session[key]);
                if (Obj == null)
                    return (T)Activator.CreateInstance(typeof(T));
                return Obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static void Clear()
        {
            _session.Clear();
        }
    }
}
