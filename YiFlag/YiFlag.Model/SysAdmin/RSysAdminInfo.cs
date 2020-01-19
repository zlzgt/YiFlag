using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class RSysAdminInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte State { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
        public int RolesId { get; set; }    //用户角色Id
        public string RolesName { get; set; }  //用户角色名
    }
}
