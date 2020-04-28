namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SMSDeviceInfo")]
    public partial class SMSDeviceInfo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string IMEI { get; set; }

        public int? DataType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string ObservedDevice { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Area { get; set; }

        public bool? IsAllDay { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool? IsEndTimeNextDay { get; set; }

        [StringLength(50)]
        public string Person1 { get; set; }

        [StringLength(50)]
        public string Phone1 { get; set; }

        [StringLength(50)]
        public string Person2 { get; set; }

        [StringLength(50)]
        public string Phone2 { get; set; }

        [StringLength(50)]
        public string Person3 { get; set; }

        [StringLength(50)]
        public string Phone3 { get; set; }

        public bool? LastStatus { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
