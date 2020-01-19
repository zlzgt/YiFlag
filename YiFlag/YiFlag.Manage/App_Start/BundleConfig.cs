using System.Web;
using System.Web.Optimization;
namespace YiFlag.Manage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/LayUI/layuiadmin/layui/css/layui.css",
                    "~/LayUI/layuiadmin/style/admin.css"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/JqueryJs").Include(
             "~/scripts/jquery-3.0.0.js"
             ));
            bundles.Add(new ScriptBundle("~/bundles/LayJs").Include(
                //   "~/LayUI/layuiadmin/lib/admin.js",
                "~/LayUI/layuiadmin/layui/DateUtils.js",
                  "~/LayUI/layuiadmin/layui/layui.js"
                ));

          
        }
    }
}
