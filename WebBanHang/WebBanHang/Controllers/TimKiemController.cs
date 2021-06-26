using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            //Tìm kiếm theo tên
            var lstSP = db.SanPham.Where(n => n.ProductName.Contains(sTuKhoa));
            return View(lstSP.OrderByDescending(n => n.ProductID));
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            //Tìm kiếm theo tên

            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa });
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