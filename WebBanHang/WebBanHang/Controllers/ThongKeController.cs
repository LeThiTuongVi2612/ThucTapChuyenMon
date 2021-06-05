using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ThongKeController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: ThongKe
        public ActionResult Index()
        {
            //lấy số người truy cập từ application đã tạo
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            //lấy số người đang onl từ application đã tạo
            ViewBag.SoNguoiDangOnl = HttpContext.Application["SoNguoiDangOnl"].ToString();
            ViewBag.TongDanhThu = ThongKeTongDT();
            ViewBag.DonHang = ThongKeDonHang();
            ViewBag.SanPhamTK = ThongKeSanPham();
            ViewBag.NCC = ThongKeNCC();
            ViewBag.NguoiDung = ThongKeNguoiDung();
            ViewBag.PhieuNhap = ThongKeNhapHang();
            return View();
        }
        public ActionResult ThongKePartial()
        {
            ViewBag.TongDanhThu = ThongKeTongDT();
            ViewBag.DonHang = ThongKeDonHang();
            ViewBag.SanPhamTK = ThongKeSanPham();
            return View();
        }

            public decimal ThongKeTongDT()
        {
            decimal TongDT = db.OrderDetail.Sum(n => n.soLuong * n.Gia).Value;
            decimal tongDT = TongDT / 1000;
            return tongDT;
        }

        public double ThongKeDonHang()
        {
            double ddh = db.Order.Count();
            return ddh;
        }

        public double ThongKeNhapHang()
        {
            double nh = db.PhieuNhapHang.Count();
            return nh;
        }

        public double ThongKeSanPham()
        {
            double sp = db.SanPham.Count();
            return sp;
        }
        public double ThongKeNCC()
        {
            double ncc = db.Company.Count();
            return ncc;
        }
        public double ThongKeNguoiDung()
        {
            double nd = db.Users.Count();
            return nd;
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