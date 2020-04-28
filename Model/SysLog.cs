namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysLog")]
    public partial class SysLog
    {
        public int ID { get; set; }

        public string Msg { get; set; }

        public string Msg2 { get; set; }

        public string Msg3 { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
