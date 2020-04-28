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
    public class AnotherController : Controller
    {
        public ActionResult Index()
        {
           
            return View();
        }
        

        string[] IMEIs = {
            "869975030497708",//鹰潭手提城市牛皮藓清洗激光器
            "869975036503749",//门磁-王老师家冰箱
            "869975036502808",//门磁-王老师家门
            "869975036502469",//门磁-3斋821A门
            "869975036504135",//门磁-机电楼418门
            "866971030672336",//2336挂锁
            "869531030024529",//4529电表
            "866971034080379" //0379挂锁
        };
        public string GetVal_All()
        {
            return DataControl.GetValues(IMEIs);
        }

        public void OpenLock(string IMEI)
        {
            DataSet ds = OR.SQLHelper.Query($"select * from DeviceInfo where IMEI='{IMEI}'", "DefaultConnection");//86697103 4080379
            string dat = "AB0B070" + IMEI.Substring(8) + "040101";
            WebClient apiClient = new WebClient();
            apiClient.Headers["Type"] = "POST";
            apiClient.Headers["Accept"] = "application/json";
            apiClient.Headers["api-key"] = ds.Tables[0].Rows[0]["APIkey"].ToString();
            apiClient.Encoding = Encoding.UTF8;
            string str = apiClient.UploadString(new Uri($"https://api.heclouds.com/nbiot/execute?imei={IMEI}&obj_id=3200&obj_inst_id=0&res_id=5750"), "{\"args\":\"" + ToolKit.CRC16(dat) + "\"}");

            //dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

            //if (Convert.ToInt32(result["errno"]) == 0)
            //{
            //    return Json(new { ErrorMsg = "" });
            //}
            //else
            //{
            //    return Json(new { ErrorMsg = result["error"].ToString() });
            //}
        }
    }
}








