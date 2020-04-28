using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace OneNETDataReceiver.Code
{
    public class DataControl
    {
        internal static string GetValues(string[] iMEIs)
        {
            string result = "";
            DataSet ds = OR.SQLHelper.Query("SELECT * FROM dbo.DeviceInfo", "DefaultConnection");

            foreach (string imei in iMEIs)
            {
                DataRow[] drs = ds.Tables[0].Select($"IMEI='{imei}'");
                if (drs.Length == 0)
                {
                    result += "2,";
                }
                else
                {
                    result += GetValue(drs[0]) + ",";
                }
            }
            return result.TrimEnd(',');
        }
        private static string GetValue(DataRow dr)
        {
            WebClient apiClient = new WebClient();
            apiClient.Headers["Type"] = "GET";
            apiClient.Headers["Accept"] = "application/json";
            apiClient.Headers["api-key"] = dr["APIkey"].ToString();
            apiClient.Encoding = Encoding.UTF8;
            string str = apiClient.DownloadString($"https://api.heclouds.com/devices/{dr["DeviceID"]}/datapoints");

            dynamic result = JsonConvert.DeserializeObject<dynamic>(str);

            if (result["errno"] == 0)
            {
                try
                {
                    return DataParse(result["data"]["datastreams"][0]["datapoints"][0]["value"].ToString(), dr["DataTypeID"].ToString());
                }
                catch
                {
                    return "2";
                }
            }
            else
            {
                return "2";
            }
        }

        private static string DataParse(string data, string dataTypeID)
        {
            dynamic result = default(dynamic);
            try
            {
                result = JsonConvert.DeserializeObject<dynamic>(data);
            }
            catch
            {

            }


            switch (dataTypeID)
            {
                case "1"://门磁
                    return result["open"].ToString();
                case "2"://开关
                    return result["open"].ToString();
                case "3"://手提清洗激光器
                    return result["open"].ToString();
                case "4"://挂锁
                    string res = "";
                    if (data[3] == 'a')//开机上报
                    {
                        res += "开机自动上报：";
                        string importantData = data.Substring(14, 6);
                        if (importantData[1] == '1')
                        {
                            res += "锁已打开，";
                        }
                        else if (importantData[1] == '2')
                        {
                            res += "锁已关闭，";
                        }
                        res += "电量:" + Convert.ToInt32(importantData.Substring(2, 2), 16) + "%，";
                        res += "信号:-" + Convert.ToInt32(importantData.Substring(4, 2), 16) + "dbm";
                    }
                    else if (data[3] == 'c')//状态变化,自动上报
                    {
                        res += "状态变化上报：";
                        if (data[15] == '1')
                        {
                            res += "锁已打开";
                        }
                        else if (data[15] == '2')
                        {
                            res += "锁已关闭";
                        }
                    }
                    else if (data[3] == 'd')//报警,AB0D09011402670186697103
                    {
                        res += "智能锁报警：";
                        if (data[15] == '1')
                        {
                            res += "撬锁警报";
                        }
                        else if (data[15] == '2')
                        {
                            res += "电量低警报";
                        }
                    }
                    else if (data[3] == 'e')//周期上报
                    {
                        res += "周期上报：";
                        if (data[15] == '1')
                        {
                            res += "锁已打开，";
                        }
                        else if (data[15] == '2')
                        {
                            res += "锁已关闭，";
                        }
                        res += "电量:" + Convert.ToInt32(data.Substring(16, 2), 16) + "%";
                    }
                    return res;
                case "5"://电表
                    return $"第{result["tx"]}次上报：电压：{result["voltage"]}V，电流：{result["current"]}A，功率：{result["activepower"]}W，频率：{result["frequency"]}Hz，PF：{result["powerfactor"]}，总用电量：{result["electricquantity"]}kWh";
                default:
                    return "";
            }
        }


        public class Response
        {
            public bool state { get; set; }
            public string message { get; set; }
        }
    }
}