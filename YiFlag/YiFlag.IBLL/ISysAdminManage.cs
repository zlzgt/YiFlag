using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.Model;

namespace YiFlag.IBLL
{
    public interface ISysAdminManage:IBaseManage
    {
        SysAdmin Login(UserInfo user);
    }
}
