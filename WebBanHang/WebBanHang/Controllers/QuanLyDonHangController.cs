using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: QuanLyDonHang
        public ActionResult ChuaThanhToan()
        {
            var lstChuaThanhToan = db.Order.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgayDat);
            return View(lstChuaThanhToan);
        }

        public ActionResult ChuaGiao()
        {
            var lstDSDHCG = db.Order.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan==true).OrderBy(n => n.NgayGiao);
            return View(lstDSDHCG);
        }

        public ActionResult DaGiaoDaThanhToan()
        {
            var lstDGDTT = db.Order.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true);
            return View(lstDGDTT);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            //kiểm tra id có hợp lệ hay không
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Order model = db.Order.SingleOrDefault(n => n.OrderID == id);
            //Kiểm tra đơn hàng có tồn tại hay không
            if(model == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.OrderDetail.Where(n => n.OrderID == id);
            ViewBag.ListCTHD = lstChiTietDH;
            return View(model);
        }
    }
}