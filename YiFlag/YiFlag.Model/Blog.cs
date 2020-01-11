namespace YiFlag.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        public string Summary { get; set; }

        public string SummaryContent { get; set; }

        public byte State { get; set; }

        public bool? IsOpen { get; set; }

        public bool? IsTop { get; set; }

        public DateTime AddTime { get; set; }

    }
}
