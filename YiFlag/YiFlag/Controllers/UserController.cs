using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YiFlag.BLL;
using YiFlag.IBLL;
using YiFlag.Model;
using YiFlag.Tools;
using Unity;
namespace YiFlag.Controllers
{
    [ManageAuthorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult SubBlog(Blog blog)
        {
            IUserManage userMange = UnityFactory.CreateInstance().Resolve<IUserManage>();
            blog.UserId = ((SysUser)Session[CurrentManage.SESSIONNAME]).Id;
            blog.AddTime = DateTime.Now;
            Blog userBlog= userMange.SubBlog(blog);
            if(userBlog!=null)
            {
                return Content("<script>alert('提交成功');history.go(-1);</script>");
            }
            else
            {
                return Content("<script>alert('提交失败');history.go(-1)</script>");
            }
        }

        public ActionResult MyBlog()
        {
            return View();
        }
        public string  MyBlogList(Page page)
        {
            page.pageSize = 10;
            IUserManage blogManage = UnityFactory.CreateInstance().Resolve<IUserManage>();
            int userId = ((SysUser)Session[CurrentManage.SESSIONNAME]).Id;
            return blogManage.MyBlogList(page, userId);
        }
        public ActionResult GetBlogDetail(int id)
        {
            IUserManage userManage = UnityFactory.CreateInstance().Resolve<IUserManage>();
            Blog blog= userManage.GetBlogDetail(id);
            return View(blog);
        }
    }
}