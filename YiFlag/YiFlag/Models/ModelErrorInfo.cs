using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YiFlag.Models
{
    public static class ModelErrorInfo
    {
        public static string GetErrorInfo(ICollection<ModelState> modelSatate)
        {
            StringBuilder errinfo = new StringBuilder();
            foreach (var s in modelSatate)
            {
                foreach (var p in s.Errors)
                {
                    errinfo.AppendFormat("{0}\\n", p.ErrorMessage);
                }
            }
            return errinfo.ToString();
        }
    }
}