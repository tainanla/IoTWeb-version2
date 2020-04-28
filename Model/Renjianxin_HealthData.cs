namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Renjianxin_HealthData
    {
        [Key]
        [StringLength(20)]
        public string UserID { get; set; }

        public double? UserTemp { get; set; }

        public int? UserHeartbeat { get; set; }
    }
}
