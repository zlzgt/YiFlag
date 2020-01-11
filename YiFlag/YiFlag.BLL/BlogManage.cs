using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.Dal;
using YiFlag.IBLL;
using YiFlag.IDal;
using YiFlag.Model;
using YiFlag.Tools;
using Unity;
using Unity.Resolution;

namespace YiFlag.BLL
{
   public class BlogManage:BaseManage,IBlogManage
    {
        private IBlogService blogService= UnityFactory.CreateInstance().Resolve<IBlogService>(new ResolverOverride[] { new ParameterOverride("context", DbContextFactory.Create()) });
        public string GetBlogList(Page page)
        {
            try
            {
                PageResult<Blog> pageResult = blogService.QueryPage<Blog, int>(w => true, page.pageSize, page.pageIndex, w => w.Id, false);
                int pageCount = (pageResult.PageSize + pageResult.TotalCount - 1) / pageResult.PageSize;
                return JsonConvert.SerializeObject(new {stauts=1,msg="获取数据成功",data =pageResult.DataList, pages = pageCount });
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}业务名称为{nameof(GetBlogList)}");
                return JsonConvert.SerializeObject(new { status = -1, msg = "出现异常" });
            }
           
        }
    }
}
