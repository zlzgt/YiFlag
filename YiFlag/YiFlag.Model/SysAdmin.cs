namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysAdmin")]
    public partial class SysAdmin
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64)]
        public string PassWord { get; set; }

        public byte State { get; set; }

        public DateTime CreateTime { get; set; }

        [StringLength(64)]
        public string Remark { get; set; }
    }
}
