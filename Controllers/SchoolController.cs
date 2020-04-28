using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OneNETDataReceiver.Code;

namespace OneNETDataReceiver.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            return View();
        }


        string[] IMEIs = { "869975030499373", "869975036665662", "869975036503749" };
        public string GetVal_All()
        {
            return DataControl.GetValues(IMEIs);
        }
    }
}