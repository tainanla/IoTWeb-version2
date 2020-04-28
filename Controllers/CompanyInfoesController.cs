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
    public class CompanyInfoesController : Controller
    {
        private IOTWeb db = new IOTWeb();

        // GET: CompanyInfoes
        public ActionResult Index()
        {
            return View(db.CompanyInfo.ToList());
        }

        // GET: CompanyInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyInfo companyInfo = db.CompanyInfo.Find(id);
            if (companyInfo == null)
            {
                return HttpNotFound();
            }
            return View(companyInfo);
        }

        // GET: CompanyInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyInfoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key_id,Company_id,Company_name")] CompanyInfo companyInfo)
        {
            if (ModelState.IsValid)
            {
                db.CompanyInfo.Add(companyInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyInfo);
        }

        // GET: CompanyInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyInfo companyInfo = db.CompanyInfo.Find(id);
            if (companyInfo == null)
            {
                return HttpNotFound();
            }
            return View(companyInfo);
        }

        // POST: CompanyInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key_id,Company_id,Company_name")] CompanyInfo companyInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyInfo);
        }

        // GET: CompanyInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyInfo companyInfo = db.CompanyInfo.Find(id);
            if (companyInfo == null)
            {
                return HttpNotFound();
            }
            return View(companyInfo);
        }

        // POST: CompanyInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompanyInfo companyInfo = db.CompanyInfo.Find(id);
            db.CompanyInfo.Remove(companyInfo);
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
