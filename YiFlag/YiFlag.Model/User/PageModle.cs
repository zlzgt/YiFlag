using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class PageModle
    {
        [Required(ErrorMessage ="页面索引不能为空")]
        public int page { get; set;}   //当前页
        [Required(ErrorMessage ="页面大小不能为空")]
        public int limit { get; set;}  //页面大小
        public string keyword { get; set;}  //搜索关键字
    }
}
