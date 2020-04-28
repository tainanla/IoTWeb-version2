namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoryValue")]
    public partial class HistoryValue
    {
        public int ID { get; set; }

        [StringLength(50)]

        public string SN { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public DateTime? ReportTime { get; set; }

        public int? RSRP { get; set; }

        public int? SNR { get; set; }

        public int? Battery { get; set; }

        public DateTime? ServerTime { get; set; }
    }
}
