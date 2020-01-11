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
    public class UserManage : BaseManage, IUserManage
    {
        private IUserService userService= UnityFactory.CreateInstance().Resolve<IUserService>(new ResolverOverride[] { new ParameterOverride("context", DbContextFactory.Create()) });
        #region 提交博客业务

        public Blog SubBlog(Blog blog)
        {
            try
            {
                Blog userBlog = userService.Insert<Blog>(blog);
                return userBlog;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}业务类为{nameof(SubBlog)}");
                return null;
            }
           
        }
        #endregion

        #region 用户注册业务
        public SysUser Register(SysUser user)
        {
            SysUser sysUser = userService.Query<SysUser>(u => u.UserName == user.UserName).FirstOrDefault();
            if (sysUser != null)
            {
                return null;
            }
            else
            {
                user.Pwd = MD5Encrypt.Encrypt(user.Pwd);
                SysUser regSysUser = userService.Insert<SysUser>(user);
                return regSysUser;
            }
        }
        #endregion

        #region 用户登录业务
        public SysUser Login(UserInfo user)
        {
            try
            {
                user.Pwd = MD5Encrypt.Encrypt(user.Pwd);
                SysUser sysUser = userService.Query<SysUser>(w => w.UserName == user.UserName && w.Pwd == user.Pwd&&w.State==1).FirstOrDefault();
                return sysUser;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}业务方法为{nameof(Login)}");
                return null;
            }
           
        }
        #endregion

        #region 我的博客列表
        public string MyBlogList(Page page, int userId)
        {
            try
            {
                PageResult<Blog> pageResult = userService.QueryPage<Blog, int>(w =>w.UserId==userId, page.pageSize, page.pageIndex, w => w.Id, false);
                int pageCount = (pageResult.PageSize + pageResult.TotalCount - 1) / pageResult.PageSize;
                return JsonConvert.SerializeObject(new { stauts = 1, msg = "获取数据成功", data = pageResult.DataList, pages = pageCount });
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}业务名为{nameof(MyBlogList)}");
                return JsonConvert.SerializeObject(new { status = -1, msg = "出现异常" });
            }
        }
        #endregion

        #region 用户获取博客详情
        public Blog GetBlogDetail(int id)
        {
            try
            {
                Blog blog = userService.Find<Blog>(id);
                return blog;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"出现异常{ex.Message}业务方法为{nameof(GetBlogDetail)}");
                return null;
            }

        }
        #endregion

    }
}
