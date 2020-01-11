namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysMenueFunction")]
    public partial class SysMenueFunction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? MenueId { get; set; }

        public int? FunctionId { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
