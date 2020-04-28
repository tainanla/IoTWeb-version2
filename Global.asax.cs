using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OneNETDataReceiver
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["UserName"] = "";
            Session["Sys_Role"] ="";
           
        }
    }
}
