using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class ReplyModel
    {
        public int code { get; set; } = -1;  //状态代码  -1错误消息 0 成功消息
        public string msg { get; set;}  //提示消息
        public Object data { get; set; } 
    }
}
