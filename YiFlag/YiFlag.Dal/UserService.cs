using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.IDal;
using YiFlag.Model;
using YiFlag.Tools;

namespace YiFlag.Dal
{
   public class UserService:BaseService,IUserService
    {
        public UserService(DbContext context) : base(context)
        {
        }
     
    }
}
