namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SysAdminRoles
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RolesId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
