using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{


    public class TreeChildViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<TreeChildViewModel> children { get; set; }
    }
    public class MenueTree
    {

        public List<TreeChildViewModel> AddChildN(int Pid)
        {
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                var data = dbContext.Set<SysMenue>().Where(x => x.ParentId == Pid).ToList();//这里是获取数据
                List<TreeChildViewModel> list = new List<TreeChildViewModel>();
                foreach (var item in data)
                {
                    //这一块主要是转换成TreeChidViewModel的值.
                    TreeChildViewModel childViewModel = new TreeChildViewModel();
                    childViewModel.id = item.Id;
                    childViewModel.title = item.Name;
                    childViewModel.children = GetChildList(childViewModel);
                    list.Add(childViewModel);
                }
                return list;
            }

        }
        public List<TreeChildViewModel> GetChildList(TreeChildViewModel treeChildView)
        {
            using (YiFlagContext dbContext = new YiFlagContext())
            {
                if (dbContext.Set<SysMenue>().Where(w => w.ParentId == treeChildView.id).ToList().Count == 0)
                {
                    return null;
                }
                else
                {
                    return AddChildN(treeChildView.id);
                }
            }
        }

        #region 根据Id判断是否有子节点
        public bool IsHasChild(int id)
        {
            using(YiFlagContext dbContext=new YiFlagContext())
            {
                if(dbContext.Set<SysMenue>().Where(w=>w.ParentId==id).ToList().Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

    }
}
