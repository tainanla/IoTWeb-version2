using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OneNETDataReceiver.Controllers
{
    public class BusinessController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"].ToString() != "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        public ActionResult GetProducts()
        {
            DataSet ds = OR.SQLHelper.Query("SELECT * FROM dbo.OneNetProduct ORDER BY id DESC", "DefaultConnection");

            List<object> list = new List<object>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new
                {
                    ID = dr["ID"],
                    Name = dr["Name"].ToString(),
                    APIkey = dr["APIkey"].ToString(),
                    DeviceCount = GetDevCount(dr["APIkey"].ToString()),
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataTypes()
        {
            DataSet ds = OR.SQLHelper.Query("SELECT * FROM dbo.DataType ORDER BY id", "DefaultConnection");

            List<object> list = new List<object>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new
                {
                    ID = dr["ID"],
                    Name = dr["Name"].ToString(),
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        private int GetDevCount(string apikey)
        {
            try
            {
                WebClient apiClient = new WebClient();
                apiClient.Headers["Type"] = "GET";
                apiClient.Headers["Accept"] = "application/json";
                apiClient.Headers["api-key"] = apikey;
                apiClient.Encoding = Encoding.UTF8;
                string str = apiClient.DownloadString(new Uri("https://api.heclouds.com/devices"));

                dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

                if (result["errno"] == 0)
                {
                    return result["data"]["total_count"];
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public ActionResult GetDevices()
        {
            string key = Request.QueryString["APIkey"];

            WebClient apiClient = new WebClient();
            apiClient.Headers["Type"] = "GET";
            apiClient.Headers["Accept"] = "application/json";
            apiClient.Headers["api-key"] = key;
            apiClient.Encoding = Encoding.UTF8;
            string str = apiClient.DownloadString(new Uri("https://api.heclouds.com/devices"));
            dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

            List<object> list = new List<object>();
            if (result["errno"] == 0)
            {
                foreach (var dr in result["data"]["devices"])
                {
                    GetLastData(key, dr["id"].ToString(), out string time, out string data);
                    list.Add(new
                    {
                        apikey = key,
                        DeviceID = dr["id"].ToString(),
                        Name = dr["title"].ToString(),
                        IMEI = dr["rg_id"].ToString(),
                        Online = (bool)dr["online"],
                        LastTime = time,
                        LastData = data,
                    });
                    //OR.SQLHelper.ExecuteSql($"UPDATE DeviceInfo SET devicename='{dr["title"]}' WHERE DeviceID='{dr["id"]}'", "DefaultConnection");
                    //OR.SQLHelper.ExecuteSql($"INSERT INTO DeviceInfo values('{dr["id"]}','{dr["title"]}','{dr["rg_id"]}','{key}',0)", "DefaultConnection");

                }
            }


            return Json(list, JsonRequestBehavior.AllowGet);
        }


        private void GetLastData(string apikey, string id, out string time, out string data)
        {
            time = "";
            data = "";
            WebClient apiClient = new WebClient();
            apiClient.Headers["Type"] = "GET";
            apiClient.Headers["Accept"] = "application/json";
            apiClient.Headers["api-key"] = apikey;
            apiClient.Encoding = Encoding.UTF8;
            string str = apiClient.DownloadString($"https://api.heclouds.com/devices/{id}/datapoints");

            dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

            if (result["errno"] == 0)
            {
                try
                {
                    time = result["data"]["datastreams"][0]["datapoints"][0]["at"];
                    data = result["data"]["datastreams"][0]["datapoints"][0]["value"];
                }
                catch { }
            }
        }

        public ActionResult AddAccount(string ProductName, string MasterAPIkey)
        {
            DataSet ds = OR.SQLHelper.Query($"SELECT 1 FROM dbo.OneNetProduct where name='{ProductName}'", "DefaultConnection");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Json(new { ErrorMsg = "名称已存在!" });
            }

            try
            {
                if (OR.SQLHelper.ExecuteSql($"insert into OneNetProduct values('{ProductName}','{MasterAPIkey}')", "DefaultConnection") == 1)
                {
                    return Json(new { ErrorMsg = "" });
                }
                else
                {
                    return Json(new { ErrorMsg = "数据错误,请重试!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { ErrorMsg = ex.Message });
            }
        }


        public ActionResult DelAccount(int rowID)
        {
            if (OR.SQLHelper.ExecuteSql($"delete dbo.OneNetProduct where id={rowID}", "DefaultConnection") > 0)
            {
                return Json(new { ErrorMsg = "" });
            }
            else
            {
                return Json(new { ErrorMsg = "删除失败!" });
            }
        }

        public ActionResult DelDevice(string apikey, string DeviceID)
        {
            WebClient apiClient = new WebClient();
            apiClient.Headers["Type"] = "POST";
            apiClient.Headers["Accept"] = "application/json";
            apiClient.Headers["api-key"] = apikey;
            apiClient.Encoding = Encoding.UTF8;
            string str = apiClient.UploadString(new Uri("https://api.heclouds.com/devices/" + DeviceID), "DELETE", "");

            dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

            if (Convert.ToInt32(result["errno"]) == 0)
            {
                OR.SQLHelper.ExecuteSql($"delete DeviceInfo where DeviceID={DeviceID}", "DefaultConnection");
                return Json(new { ErrorMsg = "" });
            }
            else
            {
                return Json(new { ErrorMsg = result["error"].ToString() });
            }
        }

        public ActionResult AddDevice(string APIkey, string DeviceName, string IMEI, string DataTypeID)
        {
            try
            {
                WebClient apiClient = new WebClient();
                apiClient.Headers["Type"] = "POST";
                apiClient.Headers["Accept"] = "application/json";
                apiClient.Headers["api-key"] = APIkey;
                apiClient.Encoding = Encoding.UTF8;
                string str = apiClient.UploadString(new Uri("https://api.heclouds.com/devices"), "{\"title\":\"" + DeviceName + "\",\"protocol\":\"LWM2M\",\"auth_info\":{\"" + IMEI + "\": \"" + IMEI + "\"}}");

                dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

                if (Convert.ToInt32(result["errno"]) == 0)
                {
                    // ;  datatype,   apikey  ,imei, 
                    OR.SQLHelper.ExecuteSql($"delete DeviceInfo where imei='{IMEI}';insert into DeviceInfo values('{result["data"]["device_id"]}','{DeviceName}','{IMEI}','{APIkey}',{DataTypeID},'2017-01-01 00:00:00.000', '0')", "DefaultConnection");
                    return Json(new { ErrorMsg = "" });
                }
                else
                {
                    return Json(new { ErrorMsg = result["error"].ToString() });
                }
            }
            catch (Exception ex)
            {
                return Json(new { ErrorMsg = ex.Message });
            }
        }
    }
}