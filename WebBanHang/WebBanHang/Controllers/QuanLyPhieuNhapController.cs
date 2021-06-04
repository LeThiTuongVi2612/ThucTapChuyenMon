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
            ViewBag.ListSanPham = db.SanPham;

            return View();
        }

        [HttpPost]
        public ActionResult NhapHang(PhieuNhapHang model, IEnumerable<ChiTietDonDangHang> ListModel)
        {

            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy");
            ViewBag.ListSanPham = db.SanPham;
            //Sau khi kiển tra dữ liệu đầu vào, gán đã xóa bằng false
            model.DaXoa = false;
            db.PhieuNhapHang.Add(model);
            db.SaveChanges();
            //SaveChange để lấy được phiếu nhập cho chi tiết phiếu nhập
            SanPham sp;
            foreach (var item in ListModel)
            {
                //Cập nhật số lượng tồn
                sp = db.SanPham.Single(n => n.ProductID == item.ProductID);
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
            var lstSP = db.SanPham.Where(n => n.SoLuongTon <= 5);
            return View(lstSP);

        }

        [HttpGet]
        public ActionResult NhaphangDon(int? id)
        {
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.CompanyID), "CompanyID", "TenCongTy");
            //Tương tự như trang chỉnh sửa sản phẩm nhưng không cần show hết các thuộc tính
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPham.SingleOrDefault(n => n.ProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        public ActionResult NhaphangDon(PhieuNhapHang pn, ChiTietDonDangHang ctpn)
        {
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.CompanyID), "CompanyID", "TenCongTy", pn.CompanyID);
            //Sau khi kiển tra dữ liệu đầu vào, gán đã xóa bằng false
            pn.DaXoa = false;
            db.PhieuNhapHang.Add(pn);
            db.SaveChanges();
            //SaveChange để lấy được phiếu nhập cho chi tiết phiếu nhập

            ctpn.MaPN = pn.MaPN;
            //Cập nhạt số lượn tồn
            SanPham sp = db.SanPham.Single(n => n.ProductID == ctpn.ProductID);
            sp.SoLuongTon += ctpn.SoLuongNhap;

            db.ChiTietDonDangHang.Add(ctpn);
            db.SaveChanges();

            return RedirectToAction("DSSPHetHang");
        }

        //Giải phóng ciến vùng nhớ
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