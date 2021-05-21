using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangOnlEntities db = new QuanLyBanHangOnlEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult OurCoffee()
        {
            var listProduct1 = (from x in db.Product select x).ToList();
            ViewBag.ListSP = listProduct1;
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult Add_Account()
        {
            return View();
        }
        public ActionResult ForGotPassWord()
        {
            return View();
        }
        public ActionResult PassWord()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Partial()
        {
            return View();
        }
    }

}