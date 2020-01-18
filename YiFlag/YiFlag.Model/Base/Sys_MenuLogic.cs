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
                //if (!this._Account.IsSuperManage)
                //{
                //    var _roleid = this._Account.RoleID.ToGuid();
                //    sql = @"
                //                        select * from (
                //                        select Menu_ID, a.Menu_Num, Menu_Name, Menu_Url, Menu_Icon, a.Menu_ParentID, Menu_CreateTime 
                //                        from (select * from Sys_Menu where 1=1 and Menu_Url is null or Menu_Url='') a
                //                         join (
                //                         select Menu_Num,Menu_ParentID
                //                          from [dbo].[Sys_RoleMenuFunction] 
                //                          join Sys_Menu on Menu_ID=RoleMenuFunction_MenuID and RoleMenuFunction_RoleID='" + _roleid + @"'
                //                          group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID,Menu_Num,Menu_ParentID
                //                        ) b on charindex(a.Menu_Num,b.Menu_Num)>0 and a.Menu_ID=b.Menu_ParentID
                //                        union
                //                        select Menu_ID, Menu_Num, Menu_Name, Menu_Url, Menu_Icon, Menu_ParentID, Menu_CreateTime 
                //                        from Sys_Menu x
                //                        join (
                //                         select RoleMenuFunction_MenuID,RoleMenuFunction_RoleID 
                //                          from [dbo].[Sys_RoleMenuFunction] 
                //                          group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID
                //                        ) y on x.Menu_ID=y.RoleMenuFunction_MenuID and y.RoleMenuFunction_RoleID='" + _roleid + @"'

                //                        ) tab
                //                        order by tab.Menu_Num asc
                //                    ";
                //}
                return dbContext.Database.SqlQuery<SysMenue>(sql, parameter).ToList();
            }
        }

        public List<SysMenue> GetUserMenus()
        {
             return  this.GetMenuByRoleID();
        }

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

        #endregion  左侧菜单

        ///// <summary>
        ///// 获取 菜单功能 树
        ///// </summary>
        ///// <returns></returns>
        //public List<object> GetMenuZTree()
        //{
        //    var list = new List<object>();
        //    var _Sys_Menu_List = db.Query<Sys_Menu>().OrderBy(item => new { item.Menu_Num }).ToList();
        //    var _Sys_Function_List = db.Query<Sys_Function>().OrderBy(item => new { item.Function_Num }).ToList();
        //    var _Sys_MenuFunction_List = db.Query<Sys_MenuFunction>().OrderBy(item => new { item.MenuFunction_CreateTime }).ToList();
        //    //遍历菜单
        //    foreach (var item in _Sys_Menu_List)
        //    {
        //        list.Add(new
        //        {
        //            id = item.Menu_ID,
        //            name = item.Menu_Name + "(" + item.Menu_Num + ")",
        //            pId = item.Menu_ParentID,
        //            @checked = false,
        //            chkDisabled = true
        //        });
        //        //判断本次菜单底下是否还有子菜单
        //        if (_Sys_Menu_List.Count(w => w.Menu_ParentID == item.Menu_ID) == 0)
        //        {
        //            //遍历功能
        //            foreach (var _Function in _Sys_Function_List)
        //            {
        //                //判断是否 该菜单下 是否勾选了 该功能
        //                var _Sys_MenuFunction_Count = _Sys_MenuFunction_List.Count(w =>
        //                w.MenuFunction_FunctionID == _Function.Function_ID &&
        //                w.MenuFunction_MenuID == item.Menu_ID);

        //                list.Add(new
        //                {
        //                    id = _Function.Function_ID,
        //                    name = _Function.Function_Name,
        //                    pId = item.Menu_ID,
        //                    @checked = _Sys_MenuFunction_Count > 0,
        //                    chkDisabled = true
        //                });
        //            }
        //        }
        //    }
        //    return list;
        //}

    }
}
