using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.IDal;
using YiFlag.Model;

namespace YiFlag.Dal
{
    public class BlogService:BaseService,IBlogService
    {
        public BlogService(DbContext context) : base(context)
        {
        }
        public string GetBlogList(Page page)
        {
            using(YiFlagContext dbContent=DbContextFactory.Create())
            {
              int Count = dbContent.Blog.Count();
              int pageCount = (Count + page.pageSize - 1) / page.pageSize;
              if(page.pageIndex<=pageCount)
                {
                  List<Blog> blogList= dbContent.Set<Blog>().OrderByDescending(w=>w.Id).Skip((page.pageIndex-1)*page.pageSize).Take(page.pageSize).ToList();
                    try
                    {
                       
                        //JsonSerializerSettings settings = new JsonSerializerSettings();
                        //settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        //return Newtonsoft.Json.JsonConvert.SerializeObject(new { data = blogList, pages = pageCount },settings);
                        return Newtonsoft.Json.JsonConvert.SerializeObject(new { data = blogList, pages = pageCount });
                    }
                    catch (Exception ex)
                    {
                        return Newtonsoft.Json.JsonConvert.SerializeObject(new { data = new List<Blog>(), pages = pageCount });
                    }
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { data =new List<Blog>(), pages = pageCount });
                }
            }
        }
        
    }
}
