using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class QuanLyPhieuNhapController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: QuanLyPhieuNhap
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy");
            ViewBag.ListSanPham = db.Product;

            return View();
        }

        [HttpPost]
        public ActionResult NhapHang(PhieuNhapHang model, IEnumerable<ChiTietDonDangHang> ListModel)
        {

            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy");
            ViewBag.ListSanPham = db.Product;
            //Sau khi kiển tra dữ liệu đầu vào, gán đã xóa bằng false
            model.DaXoa = false;
            db.PhieuNhapHang.Add(model);
            db.SaveChanges();
            //SaveChange để lấy được phiếu nhập cho chi tiết phiếu nhập
            Product sp;
            foreach (var item in ListModel)
            {
                //Cập nhật số lượng tồn
                sp = db.Product.Single(n => n.ProductID == item.ProductID);
                sp.SoLuongTon += item.SoLuongNhap;
                //sp.NgayCapNhat = DateTime.Now;
                //Gán mã phiếu nhập cho các chi tiết phiếu nhập
                item.MaPN = model.MaPN;
            }

            db.ChiTietDonDangHang.AddRange(ListModel);
            db.SaveChanges();

            return View();
        }

        [HttpGet]
        public ActionResult DSSPHetHang()
        {
            //Danh sách sản phẩm hết hàng với số lượng tồn <=5
            var lstSP = db.Product.Where(n => n.SoLuongTon <= 5);
            return View(lstSP);

        }
    }
}