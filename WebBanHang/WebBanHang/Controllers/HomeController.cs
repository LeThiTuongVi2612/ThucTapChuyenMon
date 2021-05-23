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
        QuanLyBanHangOnlEntities1 db = new QuanLyBanHangOnlEntities1();
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
            var listSp = new QuanLyBanHangOnlEntities1().GroupProduct.ToList();
            ViewBag.listGroup = listSp; 
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
        public ActionResult MenuPartial()
        {
            
            return View();
        }
        public ActionResult GroupProduct()
        {
            var listSp = new QuanLyBanHangOnlEntities1().GroupProduct.ToList();
            ViewBag.listGroup = listSp;
            return View();
        }


        

    }

}