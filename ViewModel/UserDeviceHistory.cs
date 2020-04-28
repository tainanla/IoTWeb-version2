using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.ViewModel
{
    public class UserDeviceHistory
    {
        public string Name { get; set; }
        public string DeviceName { get; set; }
        public string SN { get; set; }      
        public string Value { get; set; }
        public DateTime? ReportTime { get; set; }
        public int? RSRP { get; set; }
        public int? SNR { get; set; }
        public int? Battery { get; set; }
        public DateTime? ServerTime { get; set; }
    }
}