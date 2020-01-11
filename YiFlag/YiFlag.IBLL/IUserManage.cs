using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.Model;

namespace YiFlag.IBLL
{
    public interface IUserManage:IBaseManage
    {
        Blog SubBlog(Blog blog);
        SysUser Register(SysUser user);
        SysUser Login(UserInfo user);
        string MyBlogList(Page page, int userId);
        Blog GetBlogDetail(int id);
        
    }
}
