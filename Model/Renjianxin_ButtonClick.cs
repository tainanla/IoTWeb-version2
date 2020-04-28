namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Renjianxin_ButtonClick
    {
        [Key]
        public int ButtonID { get; set; }

        [StringLength(10)]
        public string ButtonName { get; set; }

        public int? ClickCount { get; set; }
    }
}
