namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysRolesMenueFuction")]
    public partial class SysRolesMenueFuction
    {
        public int Id { get; set; }

        public int RolesId { get; set; }

        public int MenuId { get; set; }

        public int? FuctionId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
