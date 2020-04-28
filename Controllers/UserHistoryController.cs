using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OneNETDataReceiver.ViewModel;
using OneNETDataReceiver.Model;
using System.Data;
using com.sun.tools.javac.util;

namespace OneNETDataReceiver.Controllers
{
    public class UserHistoryController : Controller
    {
        IOTWeb db = new IOTWeb();
        // GET: UserHistory
        public ActionResult Index()
        {
            string username = Session["Username"].ToString();

            {
                var UserDeviceHistoryList =
                                           from uu in db.UserInfo
                                           where uu.Username == username
                                           join ue in db.Device on uu.Company_id equals ue.Company_id
                                           where uu.Role == ue.Role
                                           join uf in db.HistoryValue on ue.SN equals uf.SN
                                           select new UserDeviceHistory
                                           {
                                               Name =uu.Username,
                                               DeviceName = ue.Device_name,
                                               SN = ue.SN,
                                               Value = uf.Value,
                                               ReportTime = uf.ReportTime,
                                               RSRP = uf.RSRP,
                                               SNR = uf.SNR,
                                               Battery = uf.Battery
                                           };

                return View(UserDeviceHistoryList);
            }
        }
        
        public ActionResult Admin ()
        {
            string username = Session["Username"].ToString();
            var UserDeviceHistoryList =
                                        from uu in db.UserInfo
                                        where uu.Username == username
                                        from ug in db.UserInfo
                                        where ug.Company_id == uu.Company_id
                                        join ue in db.Device on uu.Company_id equals ue.Company_id
                                       
                                        join uf in db.HistoryValue on ue.SN equals uf.SN
                                        select new UserDeviceHistory {
                                            Name =ug.Username,
                                            DeviceName = ue.Device_name,
                                            SN = ue.SN,
                                            Value = uf.Value,
                                            ReportTime = uf.ReportTime,
                                            RSRP = uf.RSRP,
                                            SNR = uf.SNR,
                                            Battery = uf.Battery
                                        };

            return View(UserDeviceHistoryList);
        }
    }
}