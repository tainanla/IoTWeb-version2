namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? RegisterTime { get; set; }

        public string Permission { get; set; }
        [StringLength(50)]
        

        public int Company_id { get; set; }

        
        public string Role { get; set; }
       
    }
}
