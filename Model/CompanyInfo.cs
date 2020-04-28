namespace OneNETDataReceiver.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyInfo")]
    public partial class CompanyInfo
    { 
        
        public int Company_id { get; set; }
       
        [StringLength(50)]
        public string Company_name { get; set; }
        [StringLength(50)]
        [Key]
        public string Key_id { get; set; }
       
    }
}
