using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using OneNETDataReceiver;
using System.IO;
using Newtonsoft.Json.Linq;

namespace OneNETDataReceiver.Controllers
{
    public class HomeController : Controller
    {
        private static String token = "123";//在OneNET配置的token
        private static String aeskey = "";//在OneNET配置时“消息加解密方式”选择“安全模式”下的EncodingAESKey，安全模式下必填

        public ActionResult Index()
        {
            OR.SQLHelper.ExecuteSql("insert into syslog values('收到请求,访问Home/Index','null','null',getdate())", "DefaultConnection");
            return View();
        }

        [HttpPost]
        public String receive()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string body = new StreamReader(req).ReadToEnd();
            OR.SQLHelper.ExecuteSql("insert into syslog values('data receive: body','" + body + "','null',getdate())", "DefaultConnection");
            //解析数据推送请求，非加密模式
            var obj = Util.resolveBody(body, false);

            if (obj != null)
            {
                var dataRight = Util.checkSignature(obj, token);
                if (dataRight)
                {
                    OR.SQLHelper.ExecuteSql("insert into syslog values('data receive: content','" + obj.ToString() + "','null',getdate())", "DefaultConnection");
                    JObject jsonBody = JObject.Parse(body);
                    if (!string.IsNullOrEmpty(jsonBody["msg"]["value"].ToString()))
                    {
                        JObject jsonValue = JObject.Parse(jsonBody["msg"]["value"].ToString());
                        string imei = jsonBody["msg"]["imei"].ToString();
                        string alarm = jsonValue["alarm"].ToString();
                        OR.SQLHelper.ExecuteSql($"insert into DeviceValue values('{imei}','{alarm}',getdate())", "DefaultConnection");
                        OR.SQLHelper.ExecuteSql("insert into syslog values('转换成功','null','null',getdate())", "DefaultConnection");
                    }
                    else
                    {
                        OR.SQLHelper.ExecuteSql("insert into syslog values('value为空,抛弃','null','null',getdate())", "DefaultConnection");
                    }
                }
                else
                {
                    OR.SQLHelper.ExecuteSql("insert into syslog values('signature error','null','null',getdate())", "DefaultConnection");
                }
            }
            else
            {
                OR.SQLHelper.ExecuteSql("insert into syslog values('body empty error','null','null',getdate())", "DefaultConnection");
            }

            // 解析数据推送请求，加密模式
            //Util.BodyObj obj1 = Util.resolveBody(body, true);
            //Console.WriteLine("data receive:  body --- " + obj1);
            //if (obj1 != null)
            //{
            //    bool dataRight1 = Util.checkSignature(obj1, token);
            //    if (dataRight1)
            //    {
            //        String msg = Util.decryptMsg(obj1, aeskey);
            //        Console.WriteLine("data receive: content" + msg);
            //    }
            //    else
            //    {
            //        Console.WriteLine("data receive:  signature error ");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("data receive: body empty error");
            //}
            return "ok";
        }

        public String receive(string msg, string nonce, string signature)
        {
            OR.SQLHelper.ExecuteSql("insert into syslog values('调用receive','3个参数的方法','null',getdate())", "DefaultConnection");

            if (string.IsNullOrEmpty(msg))
            {
                return "msg is null";
            }

            if (string.IsNullOrEmpty(nonce))
            {
                return "nonce is null";
            }

            if (string.IsNullOrEmpty(msg))
            {
                return "signature is null";
            }

            if (Util.VerifySignature(msg, nonce, signature, token))
            {
                return msg;
            }
            else
            {
                return "error";
            }
        }
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}