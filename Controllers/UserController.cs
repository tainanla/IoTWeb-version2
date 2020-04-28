using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using OneNETDataReceiver.Code;
using OneNETDataReceiver.ViewModel;
using OneNETDataReceiver.Model;
namespace OneNETDataReceiver.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            DataSet ds = OR.SQLHelper.Query(string.Format("SELECT * FROM dbo.UserInfo ", form["Username"], ToolKit.GetMD5String(form["Password"]).ToLower()), "DefaultConnection");
            
                if (ds.Tables[0].Rows.Count != 0)
                
                {
                //登陆成功操作
                Session["UserName"] = form["Username"];
                Session["Permission"] = form["Permission"];
                FormsAuthentication.SetAuthCookie(form["Username"], false);
                //Session["Permission"] = ds.Tables[0].Rows[0]["Permission"];
                string per = Session["Permission"].ToString();
                int permission = Convert.ToInt32(per);
                if (permission ==0)
                return RedirectToAction("Index", "UserInfoes"); 
                else if(permission == 1)return RedirectToAction("CompanyAdmin", "UserRole");
                else return RedirectToAction("Index", "UserRole");
            }
            else
            {//登陆失败操作
                ViewBag.LoginState = "失败";
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View();
            }
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
       

        
    }
}