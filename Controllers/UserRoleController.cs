using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OneNETDataReceiver.Model;
using OneNETDataReceiver.ViewModel;
using System.Net;


namespace OneNETDataReceiver.Controllers
{
    public class UserRoleController : Controller
    {
       IOTWeb db = new IOTWeb();
        // GET: UserRole
       
            public ActionResult Index()
        {
            string username = Session["Username"].ToString();
            //if (permission == 2)
                var userRoleList =
                                        from uu in db.UserInfo
                                        where uu.Username == username
                                        join ud in db.CompanyInfo on uu.Company_id equals ud.Company_id
                                        join ue in db.Device on uu.Company_id equals ue.Company_id
                                        where uu.Role == ue.Role

                                        select new UserRole { ID=uu.ID,userName = uu.Username, userComapny = ud.Company_name, userRole = ue.Role, userSN = ue.SN };
            
                return View(userRoleList);
        }
        public ActionResult CompanyAdmin()
        {
            string username = Session["Username"].ToString();
           
            var userRoleList =
                                    from uu in db.UserInfo where uu.Username == username 
                                    from uf in db.UserInfo
                                    where uu.Company_id == uf.Company_id                                  
                                    join ud in db.CompanyInfo on uu.Company_id equals ud.Company_id 
                                    //join ue in db.Device on uu.Company_id equals ue.Company_id                                                                      
                                    select new UserRole { ID = uu.ID,userName = uf.Username, userComapny = ud.Company_name,userRole = uf.Role};//,userSN = ue.SN   

            return View(userRoleList);


        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,RegisterTime,Permission,Company_id,Role")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                var entry = db.Entry(userInfo);
                entry.Property(e => e.Company_id).IsModified = true;
                entry.Property(e => e.Role).IsModified = true;
                entry.Property(e => e.Permission).IsModified = true;
                entry.Property(e => e.RegisterTime).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        //管理员
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userInfo = db.UserInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "ID,Username,Password,RegisterTime,Permission,Company_id,Role")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInfo).State = EntityState.Modified;
                var entry = db.Entry(userInfo);
                entry.Property(e => e.Company_id).IsModified = true;
                entry.Property(e => e.Permission).IsModified = true;
                entry.Property(e => e.RegisterTime).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("CompanyAdmin");
            }
            return View(userInfo);
        }
    }
}