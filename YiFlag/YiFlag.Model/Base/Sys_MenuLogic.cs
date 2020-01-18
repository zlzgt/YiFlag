using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using YiFlag.Model;
using YiFlag.Tools;

namespace AuthorityYiFlag.Model
{
    public class Sys_MenuLogic
    {
        #region  创建系统左侧菜单
        /// <summary>
        /// 根据角色ID 获取菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenue> GetMenuByRoleID()
        {
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                var sql = @"select * from SysMenue where 1=1 and state=1 order by sort asc";

                Account account = SessionHelper.GetSession<Account>("account");
                SqlParameter[] parameter = new SqlParameter[] {
                        new SqlParameter("RolesId",account.RoleID),
                        new SqlParameter("sysAdminId",account.UserID),
                    };
                if (!account.IsSuperManage)
                {
                    sql = @"select * from SysMenue where Id in( select MenuId from  SysRolesMenueFuction where RolesId =@RolesId) 
                            union
                            select* from SysMenue where Id in(select SysMenueId from SysAdminMenue where sysAdminId =@sysAdminId)";
                }
                return dbContext.Database.SqlQuery<SysMenue>(sql, parameter).ToList();
            }
        }

        public List<SysMenue> GetUserMenus()
        {
             return  this.GetMenuByRoleID();
        }

        #endregion

        #region

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="_StringBuilder"></param>
        public void CreateMenus(int Id, StringBuilder _StringBuilder)
        {
            var _Sys_Menu_List = this.GetMenuByRoleID();
            var _Parent_List = new List<SysMenue>();
            if (Id == 0)
                _Parent_List = _Sys_Menu_List.Where(w => w.ParentId == 0).ToList();
            else
                _Parent_List = _Sys_Menu_List.Where(w => w.ParentId == Id).ToList();
            if (_Parent_List.Count > 0)
            {
                if (Id != 0)
                    _StringBuilder.Append("<dl class=\"layui-nav-child\">");
                foreach (var item in _Parent_List)
                {
                    var _Child_List = _Sys_Menu_List.Where(w => w.ParentId != 0 && w.ParentId == item.Id).ToList();
                    if (item.ParentId == 0)
                    {
                        _StringBuilder.Append("<li data-name=\"home\" class=\"layui-nav-item\">");
                    }
                    if (_Child_List.Count > 0)
                    {
                        if (Id == 0)
                        {
                            _StringBuilder.Append(string.Format(" <a href=\"javascript:;\" lay-tips=\"{0}\" lay-direction=\"2\"><i class=\"{1}\"></i> <cite>{0}</cite> </a>", item.Name, item.Icon));
                        }
                        else
                        {
                            _StringBuilder.Append(string.Format("<a href=\"javascript:;\">{1}</a>", "", item.Name));
                        }
                        this.CreateMenus(item.Id, _StringBuilder);
                        _StringBuilder.Append("</dl>");
                    }
                    else
                    {
                        if (Id == 0)
                        {
                            _StringBuilder.Append(string.Format("<a href=\"javascript:;\" lay-tips=\"{0}\" lay-direction=\"2\"><i class=\"{1}\"></i> <cite>{0}</cite> </a>", item.Name, item.Icon));
                        }
                        else
                        {
                            _StringBuilder.Append(string.Format("<dd><a lay-href=\"{0}\">{1}</a></dd>", item.Url, item.Name));
                        }
                    }
                    if (item.ParentId == 0)
                    {
                        _StringBuilder.Append("</li>");
                    }
                }
            }

        }
        #endregion


    }
}
