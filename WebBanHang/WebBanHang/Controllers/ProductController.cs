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
        QuanLyBanHangOnlEntities1 db = new QuanLyBanHangOnlEntities1();
        // GET: Product

        public ActionResult Breakfast()
        {
            var listBreafast = db.Product.Where(x => x.GroupProductID == 2);
            ViewBag.ListAS = listBreafast;
            var listSp = new QuanLyBanHangOnlEntities1().GroupProduct.ToList();
            ViewBag.listGroup = listSp;

            return View();
        }

        public ActionResult Lunch()
        {
            var listLuch = db.Product.Where(x => x.GroupProductID == 3);
            ViewBag.ListAT = listLuch;
            var listSp = new QuanLyBanHangOnlEntities1().GroupProduct.ToList();
            ViewBag.listGroup = listSp;

            return View();
        }
        public ActionResult Product1()
        {

            var listSp = new QuanLyBanHangOnlEntities1().GroupProduct.ToList();
            ViewBag.listGroup = listSp;

            var listProduct1 = db.Product.Where(x => x.GroupProductID == 1);
            ViewBag.ListSP = listProduct1;

            return View();
        }
        public ActionResult ProductPartial(int id)
        {

            var listSp = (from c in db.Product where c.GroupProductID == id select c).ToList();

            return View(listSp);
        }
        public ActionResult GroupProduct()
        {
            
            return View();
        }
    }
}