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
using static OneNETDataReceiver.Code.DataControl;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OneNETDataReceiver.Controllers
{
    public class ApiDataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateDatabase()
        {
            return View();
        }


        public JsonResult UpdateDB()
        {
            Response rsq = new Response();
            //获取历史数据
            UpdateHistoryValue();
            rsq.state = true;
            rsq.message = "success";
            return Json(rsq);

        }

        public void UpdateHistoryValue()
        {
            DataSet ds = OR.SQLHelper.Query("SELECT * FROM dbo.DeviceInfo WHERE DataTypeID = 1 OR DataTypeID = 3", "DefaultConnection");
            DataRow[] drs = ds.Tables[0].Select();
            //已获取到所有待更新的设备列表
            foreach (DataRow dr in drs)
            {
                //提取某设备用来获取数据的信息
                string devideID = dr["DeviceID"].ToString();
                string SN = dr["IMEI"].ToString();
                string apiKey = dr["APIkey"].ToString();
                string lastUpdateTime = Convert.ToDateTime(dr["LastUpdateTime"]).ToString("yyyy-MM-ddTHH:mm:ss");
                string cursor = "";
                string data = "";
                //获取历史数据流
                do
                {
                    data = GetHistoryData(devideID, apiKey, lastUpdateTime, 100, cursor).message;
                    cursor = ParseHistoryData(SN, data);
                } 
                while (!string.IsNullOrEmpty(cursor));

            }
        }

        private static string ParseHistoryData(string SN, string data)
        {
            dynamic result = JsonConvert.DeserializeObject<dynamic>(data);
            //
            if (result["errno"] == 0)
            {
                string cursor = result["data"]["cursor"] ?? "";
                if (result["data"]["count"] != 0)
                {
                    foreach (var datapoint in result["data"]["datastreams"][0]["datapoints"])
                    {
                        string serverTime = datapoint["at"].ToString();
                        dynamic databody = JsonConvert.DeserializeObject<dynamic>(datapoint["value"].ToString());
                        if (!string.IsNullOrEmpty((databody ?? "").ToString()))
                        {
                            try
                            {
                                string Value = (databody["open"] ?? "").ToString();
                                DateTime.TryParseExact(databody["time"].ToString(), "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out DateTime Time);
                                string reportTime;
                                if (string.Equals(Time.ToString(), "0001/1/1 0:00:00"))
                                {
                                    reportTime = databody["time"].ToString().Replace(" ", " ");
                                }
                                else
                                {
                                    reportTime = Time.ToString("yyyy-MM-dd HH:mm:ss.000");
                                }
                                string RSRP = (databody["rsrp"] ?? "").ToString();
                                string SNR = (databody["snr"] ?? "").ToString();
                                string Battery = (databody["battery"] ?? "").ToString();
                                OR.SQLHelper.ExecuteSql($"INSERT INTO HistoryValue VALUES('{SN}','{Value}','{reportTime}','{RSRP}','{SNR}','{Battery}','{serverTime}')", "DefaultConnection");

                            }
                            catch (Exception)
                            {
                                OR.SQLHelper.ExecuteSql($"INSERT INTO SysLog VALUES('Insert HistoryValue Item Failed: {databody.ToString()}','null','null',getdate())", "DefaultConnection");
                            }
                        }
                    }
                    DataSet ds = OR.SQLHelper.Query($"SELECT DATEADD(ss, 1, MAX(ServerTime)) AS 'T' FROM HistoryValue WHERE SN = '{SN}'", "DefaultConnection");
                    string lateUpdate = ds.Tables[0].Rows[0]["T"].ToString();
                    OR.SQLHelper.ExecuteSql($"UPDATE DeviceInfo SET LastUpdateTime = '{lateUpdate}' WHERE IMEI = '{SN}'", "DefaultConnection");
                }
                
                return cursor;
            }
            else
            {
                OR.SQLHelper.ExecuteSql($"INSERT INTO SysLog VALUES('Insert HistoryValue Data Failed: {data}','null','null',getdate())", "DefaultConnection");
                return "";
            }
        }

        // 调用接口获取历史数据，文档116页
        private static Response GetHistoryData(string deviceID, string apiKey, string start, int limit, string cursor)
        {
            Response rsp = new Response();
            try
            {
                WebClient apiClient = new WebClient();
                apiClient.Headers["Type"] = "GET";
                apiClient.Headers["Accept"] = "application/json";
                apiClient.Headers["api-key"] = apiKey;
                apiClient.Encoding = Encoding.UTF8;
                string str = "";
                if (string.IsNullOrEmpty(cursor))
                {
                    str = apiClient.DownloadString(new Uri(string.Format("http://api.heclouds.com/devices/{0}/datapoints?start={1}&limit={2}", deviceID, start, limit)));
                }
                else
                {
                    str = apiClient.DownloadString(new Uri(string.Format("http://api.heclouds.com/devices/{0}/datapoints?start={1}&limit={2}&cursor={3}", deviceID, start, limit, cursor)));
                }
                
                rsp.state = true;
                rsp.message = str;
            }
            catch (Exception e)
            {
                rsp.state = false;
                rsp.message = e.Message;
            }
            return rsp;
        }

        [HttpGet]
        public string Get()
        {
            return DateTime.Now.Date.ToShortDateString(); ;
        }

        [HttpPost]
        public string Post()
        {
            return Request["id1"] + "end";
        }
    }


}