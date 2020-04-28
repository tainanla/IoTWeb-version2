using OneNETDataReceiver.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneNETDataReceiver.Controllers
{
    public class CheJianController : Controller
    {
        // GET: School
        public ActionResult Index()
        {
            return View();
        }

        string[] IMEIs = { "869975036665662", "869975036665688", "869975036665878", "869975030499373", "869975036509753", "869975036587072", "869975036500000", "869975036497918" };
        public string GetVal_All()
        {
            return DataControl.GetValues(IMEIs);
        }

    }
}