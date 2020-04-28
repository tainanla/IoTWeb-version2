using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OneNETDataReceiver.Model;

namespace OneNETDataReceiver.Controllers
{
    public class LasersController : Controller
    {
        private IOTWeb db = new IOTWeb();

        // GET: Lasers
        public ActionResult Index()
        {
            return View(db.Laser.ToList());
        }

        // GET: Lasers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laser laser = db.Laser.Find(id);
            if (laser == null)
            {
                return HttpNotFound();
            }
            return View(laser);
        }

        // GET: Lasers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lasers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Introduce,Company,Url,Contact_Address,Contact_Phone,Contact_Tele,Contact_Fax,Contact_Name")] Laser laser)
        {
            if (ModelState.IsValid)
            {
                db.Laser.Add(laser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laser);
        }

        // GET: Lasers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laser laser = db.Laser.Find(id);
            if (laser == null)
            {
                return HttpNotFound();
            }
            return View(laser);
        }

        // POST: Lasers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Introduce,Company,Url,Contact_Address,Contact_Phone,Contact_Tele,Contact_Fax,Contact_Name")] Laser laser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laser);
        }

        // GET: Lasers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laser laser = db.Laser.Find(id);
            if (laser == null)
            {
                return HttpNotFound();
            }
            return View(laser);
        }

        // POST: Lasers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Laser laser = db.Laser.Find(id);
            db.Laser.Remove(laser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
