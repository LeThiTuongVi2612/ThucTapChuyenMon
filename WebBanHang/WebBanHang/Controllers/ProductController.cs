using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        QuanLyBanHangOnlEntities db = new QuanLyBanHangOnlEntities();
        // GET: Product

        public ActionResult Breakfast()
        {
            var listBreafast = db.Product.Where(x => x.GroupProductID == 2);
            ViewBag.ListAS = listBreafast;


            return View();
        }
        public ActionResult BreakfastPratial()
        {
            


            return BreakfastPratial();
        }

        public ActionResult Product1()
        {
            var listBreafast = db.Product.Where(x => x.GroupProductID == 2);
            ViewBag.ListAS = listBreafast;

            var listProduct1 = db.Product.Where(x => x.GroupProductID == 1);
            ViewBag.ListSP = listProduct1;

            return View();
        }
        public ActionResult ProductPartial()
        {
            
            return ProductPartial();
        }
        public ActionResult GroupProduct()
        {
            var listProduct2 = db.Product.Select(x=>x);
            ViewBag.ListSP2 = listProduct2;

            

            return View();
        }
        public ActionResult GroupPartial()
        {
            //var listProduct1 = db.Product.Where(x => x.GroupProductID == 1);
            //return View(listProduct1);
            return GroupPartial();
        }
    }
}