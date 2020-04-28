using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneNETDataReceiver.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(string username, string password)
        {
            if (username == "xixixi" && password == "ll0513015379")
            {
                Session["AdminUser"] = username;
                return RedirectToAction("Index","Order", new { area = "Admin" });
            }
            else
                ViewBag.Error = "用户名或密码不正确！";
            return View();
        }

        public ActionResult LogOff()
        {
            if (Session["AdminUser"] != null)
            {
                Session["AdminUser"] = null;
            }
            return RedirectToAction("Logon");
        }
    }
}