namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SysRoles
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string RolesName { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
