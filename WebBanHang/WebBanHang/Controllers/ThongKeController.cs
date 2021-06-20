using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị, Thống kê")]
    public class ThongKeController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: ThongKe
        public ActionResult Index()
        {
            int nam = 2021;
            
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
            ViewBag.NhoomSanPham = ThongKeNhomSP();
            ViewBag.ListSanPham = db.SanPham;
            #region doanhthuban
            ViewBag.ThangMot = ThongKeDoanhThuThangMot(nam);
            ViewBag.ThangHai = ThongKeDoanhThuThangHai(nam);
            ViewBag.ThangBa = ThongKeDoanhThuThangBa(nam);
            ViewBag.ThangTu = ThongKeDoanhThuThangTu(nam);
            ViewBag.ThangNam = ThongKeDoanhThuThangNam(nam);
            ViewBag.ThangSau = ThongKeDoanhThuThangSau(nam);
            ViewBag.ThangBay = ThongKeDoanhThuThangBay(nam);
            ViewBag.ThangTam = ThongKeDoanhThuThangTam(nam);
            ViewBag.ThangChin = ThongKeDoanhThuThangChin(nam);
            ViewBag.ThangMuoi = ThongKeDoanhThuThangMuoi(nam);
            ViewBag.Thang11 = ThongKeDoanhThuThang11(nam);
            ViewBag.Thang12 = ThongKeDoanhThuThang12(nam);
            #endregion

            #region sotienmua
            ViewBag.Mua1 = ThongKeMuaThang1(nam);
            ViewBag.Mua2 = ThongKeMuaThang2(nam);
            ViewBag.Mua3 = ThongKeMuaThang3(nam);
            ViewBag.Mua4 = ThongKeMuaThang4(nam);
            ViewBag.Mua5 = ThongKeMuaThang5(nam);
            ViewBag.Mua6 = ThongKeMuaThang6(nam);
            ViewBag.Mua7 = ThongKeMuaThang7(nam);
            ViewBag.Mua8 = ThongKeMuaThang8(nam);
            ViewBag.Mua9 = ThongKeMuaThang9(nam);
            ViewBag.Mua10 = ThongKeMuaThang10(nam);
            ViewBag.Mua11 = ThongKeMuaThang11(nam);
            ViewBag.Mua12 = ThongKeMuaThang12(nam);
            #endregion
            return View();
        }
        #region ThongKeDoanhThu

        public decimal ThongKeDoanhThuThangMot( int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 1 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach(var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }

        public decimal ThongKeDoanhThuThangHai(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 2 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangBa(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 3 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangTu(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 4 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangNam(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 5 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangSau(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 6 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangBay(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 7 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangTam(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 8 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangChin(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 9 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThangMuoi(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 10 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThang11(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 11 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeDoanhThuThang12(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.Order.Where(n => n.NgayDat.Value.Month == 12 && n.NgayDat.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.OrderDetail.Sum(n => n.soLuong * n.Gia).Value.ToString());
            }
            return tongtien;
        }
        #endregion

        #region ThongKeMua
        public decimal ThongKeMuaThang1(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 1 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang2(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 2 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang3(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 3 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang4(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 4 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang5(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 5 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang6(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 6 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang7(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 7 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang8(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 8 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang9(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 9 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang10(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 10 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang11(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 11 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        public decimal ThongKeMuaThang12(int nam)
        {
            //list ra những đơn hàng có tháng năm tuong ứng
            var listDDH = db.PhieuNhapHang.Where(n => n.NgayNhap.Value.Month == 12 && n.NgayNhap.Value.Year == nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.ChiTietDonDangHang.Sum(n => n.SoLuongNhap * n.DonGiaNhap).Value.ToString());
            }
            return tongtien;
        }
        #endregion
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
        public double ThongKeNhomSP()
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