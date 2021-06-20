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
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: Product
        public ActionResult CachCheBienPartial(int id) {
            var listSp = (from c in db.SanPham where c.ProductID == id select c).ToList();
            
            return View(listSp);
        }
        public ActionResult ProductPartial(int id)
        {

            var listSp = (from c in db.SanPham where c.GroupProductID == id select c).ToList();

            return View(listSp);
        }
        public ActionResult GroupProduct()
        {
            
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}