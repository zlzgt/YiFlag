using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.IDal;

namespace YiFlag.Dal
{
    public class SysAdminService : BaseService, ISysAdminService
    {
        public SysAdminService(DbContext context) : base(context)
        {
        }
    }
}
