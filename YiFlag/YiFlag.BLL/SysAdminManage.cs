using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.IBLL;
using YiFlag.IDal;
using YiFlag.Model;
using YiFlag.Tools;
using Unity;
using Unity.Resolution;

namespace YiFlag.BLL
{
    public class SysAdminManage : BaseManage, ISysAdminManage
    {
        public SysAdmin Login(UserInfo user)
        {
            try
            {
                ISysAdminService sysAdminService = UnityFactory.CreateInstance().Resolve<ISysAdminService>(new ResolverOverride[] { new ParameterOverride("context", DbContextFactory.Create()) });
                user.Pwd = MD5Encrypt.Encrypt(user.Pwd);
                SysAdmin sysAdmin= sysAdminService.Query<SysAdmin>(w => w.UserName == user.UserName && w.PassWord == user.Pwd&&w.State==1).FirstOrDefault();

                var usersRoles = sysAdminService.Query<SysAdminRoles>(w => w.UserId == sysAdmin.Id).FirstOrDefault();
                var roles = sysAdminService.Query<SysRoles>(w => w.Id == usersRoles.RolesId).FirstOrDefault();
                var _account = new Account();
                _account.RoleID = roles.Id;
                _account.UserID = sysAdmin.Id;
                _account.UserName = sysAdmin.UserName;
                if (sysAdmin.Id == 1)
                {
                    _account.IsSuperManage = true;
                }
                SessionHelper.SetSession("account", _account);
                return sysAdmin;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog($"登录时出现异常{ex.Message}",Log4NetLevel.Error);
                return null;
            }
        }
    }
}
