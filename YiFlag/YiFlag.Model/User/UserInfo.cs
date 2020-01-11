using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class UserInfo
    {
        [Required(ErrorMessage ="用户名不能为空")]
        [StringLength(32,ErrorMessage ="用户名长度不能超过32个字符")]
        public string UserName { get; set; }

        [Required(ErrorMessage="密码不能为空")]
        [StringLength(32, ErrorMessage = "用户名长度不能超过32个字符")]
        public string Pwd { get; set; }
        //[Required(ErrorMessage ="验证码不能为空")]
        public string VerificationCode { get; set;}
    }
}
