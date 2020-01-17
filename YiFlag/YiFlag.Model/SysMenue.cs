namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysMenue")]
    public partial class SysMenue
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(64)]
        public string Icon { get; set; }

        [StringLength(128)]
        public string Url { get; set; }

        [StringLength(64)]
        public string Code { get; set; }

        public byte State { get; set; }

        public int ParentId { get; set; }

        public int Level { get; set; }

        public int Sort { get; set; }

        [StringLength(256)]
        public string Path { get; set; }
    }
}
