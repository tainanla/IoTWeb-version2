using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OneNETDataReceiver.ViewModel;
using OneNETDataReceiver.Model;
using System.Data.Entity;
using System.Net;

namespace OneNETDataReceiver.Controllers
{
    public class CompanyDeviceController : Controller
    {
        IOTWeb db = new IOTWeb();
        // GET: CompanyDevice
        [Authorize]
        public ActionResult Index()
        {
            string username = Session["Username"].ToString();
            var userDeviceList =
                                        from uu in db.UserInfo
                                        where uu.Username == username
                                        join ue in db.Device on uu.Company_id equals ue.Company_id
                                        where uu.Role == ue.Role
                                        select new UserDevice { DeviceName=ue.Device_name, UserSN = ue.SN, DeviceValue = ue.Value, Time = ue.CreateTime };

            return View(userDeviceList);            
        }
        [Authorize]
        public ActionResult Admin()
        {
            string username = Session["Username"].ToString();
            var userDeviceList =
                                        from uu in db.UserInfo
                                        where uu.Username == username
                                        join ue in db.Device on uu.Company_id equals ue.Company_id
                                        select new UserDevice { ID=ue.ID,DeviceName = ue.Device_name, UserSN = ue.SN, DeviceValue = ue.Value, Time = ue.CreateTime };

            return View(userDeviceList);
        }
      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Device.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SN,Value,Device_name,CreateTime,Company_id,Role")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(device);
        }
    }
}