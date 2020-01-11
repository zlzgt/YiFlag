using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class Page
    {
        public int pageIndex{ get; set;}
        public int pageSize { get; set; }
        public int pageCount { get; set;}
        public int pageCurrent { get; set; }
    }
}
