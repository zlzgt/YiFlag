using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
    public class MenuModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(64)]
        public string Icon { get; set; }

        [StringLength(128)]
        public string Url { get; set; }

        [StringLength(64)]
        public string Code { get; set; }

        public int ParentId { get; set; }

        public int Level { get; set; }

        public int Sort { get; set; }

        [StringLength(256)]
        public string Path { get; set; }
    }
}
