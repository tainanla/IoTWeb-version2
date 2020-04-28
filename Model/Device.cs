namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Device")]
    public partial class Device
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string SN { get; set; }

        [StringLength(500)]
        public string Value { get; set; }
        public string Device_name { get; set; }
        public DateTime? CreateTime { get; set; }
        public int Company_id { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        
    }
}
