using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiFlag.Model;

namespace YiFlag.IDal
{
    public interface IBlogService:IBaseService
    {
        string GetBlogList(Page page);
    }
}
