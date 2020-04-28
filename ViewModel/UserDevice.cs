using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.ViewModel
{
    public class UserDevice
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public string UserSN { get; set; }
        public string DeviceValue { get; set; }
        public DateTime? Time { get; set; }
    }
}