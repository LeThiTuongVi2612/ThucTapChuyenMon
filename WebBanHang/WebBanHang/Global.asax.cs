using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["SoNguoiTruyCap"] = 0;
            Application["SoNguoiDangOnl"] = 0;

        }

        protected void Session_Start()
        {
            Application.Lock();//Dùng để đồng bộ hóa
            Application["SoNguoiTruyCap"] = (int)Application["SoNguoiTruyCap"] + 1;
            Application["SoNguoiDangOnl"] = (int)Application["SoNguoiDangOnl"] + 1;
            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();//Dùng để đồng bộ hóa
            
            Application["SoNguoiDangOnl"] = (int)Application["SoNguoiDangOnl"] - 1;
            Application.UnLock();
        }
    }
}
