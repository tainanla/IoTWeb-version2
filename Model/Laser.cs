namespace OneNETDataReceiver.Model
{    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Laser")]
    public partial class Laser
    {
        [Key]
        public string Name { get; set; }

        [StringLength(50)]

        public string Introduce { get; set; }

        [StringLength(500)]

        public string Company { get; set; }

        [StringLength(50)]
        
        public string Url { get; set; }
        
        [StringLength(50)]
        public string Contact_Address { get; set; }

        [StringLength(500)]
        public string Contact_Phone { get; set; }

        [StringLength(50)]
        public string Contact_Tele { get; set; }

        [StringLength(50)]
        public string Contact_Fax { get; set; }

        [StringLength(50)]
       public string Contact_Name { get; set; }

        

    }
}
