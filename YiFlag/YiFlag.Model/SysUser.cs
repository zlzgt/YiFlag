namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysUser")]
    public partial class SysUser
    {
     
        public SysUser()
        {
           
        }

        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Pwd { get; set; }

        [StringLength(32)]
        public string Email { get; set; }

        [StringLength(8)]
        public string TrueName { get; set; }

        public bool? Sex { get; set; }

        [StringLength(11)]
        public string Tel { get; set; }

        [StringLength(64)]
        public string Addr { get; set; }

        public byte State { get; set; }

        public DateTime? AddTime { get; set; }

    }
}
