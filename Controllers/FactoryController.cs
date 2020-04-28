using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneNETDataReceiver.Controllers
{
    public class FactoryController : Controller
    {
        // GET: Factory
        public ActionResult Index()
        {
            return View();
        }

        public string GetVal()
        {
            OR.SQLHelper.ExecuteSql("insert into syslog values('收到请求,访问Factory/Index','null','null',getdate())", "DefaultConnection");
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public bool GetVal_9373()
        {
            DataSet ds = OR.SQLHelper.Query("SELECT TOP 1 * FROM dbo.DeviceValue WHERE sn='869975030499373' ORDER BY id DESC", "DefaultConnection");
            try
            {
                return ds.Tables[0].Rows[0]["Value"].ToString() == "0";
            }
            catch
            {
                return false;
            }
        }

        public double GetVal_V()
        {
            return new Random().Next(2190, 2220) * 0.1;
        }
        public double GetVal_I()
        {
            return new Random().Next(33, 37) * 0.1;
        }
        public string GetVal_GPS()
        {
            return "GPS坐标(000.000,000.000):北京市海淀区西三环北路50号豪柏大厦C1座";
        }
    }
}