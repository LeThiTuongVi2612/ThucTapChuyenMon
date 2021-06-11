using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        //Lấy giỏ hàng
        QuanLyBanHang db = new QuanLyBanHang();
        public List<ItemCart> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemCart> lsGioHang = Session["GioHang"] as List<ItemCart>;
            if(lsGioHang == null)
            {
                //Nếu session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lsGioHang = new List<ItemCart>();
                Session["GioHang"] = lsGioHang;
            }
            return lsGioHang;
        }

        //Thêm giỏ hàng thông thường(load lại trang)
        public ActionResult ThemGioHang(int productID, string strURL)
        {
            //Kiểm tr ẩn phẩm có tồn tại trong CSDL hay không 
            SanPham product = db.SanPham.SingleOrDefault(n => n.ProductID == productID);
            if(product == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng
            List<ItemCart> lsGioHang = LayGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemCart spCheck = lsGioHang.SingleOrDefault(n => n.productID == productID);
            if(spCheck != null)
            {
                //Kiểm tra số lượng tồn trướ khi cho khách hàng mua
                if(product.SoLuongTon < spCheck.soLuong)
                {
                    return View("ThongBao");
                }
                spCheck.soLuong++;
                spCheck.thanhTien = spCheck.soLuong * spCheck.Gia;
                return Redirect(strURL);
            }
            ItemCart itemGH = new ItemCart(productID);
            if (product.SoLuongTon < itemGH.soLuong)
            {
                return View("ThongBao");
            }
            lsGioHang.Add(itemGH);
            
            return Redirect(strURL);
        }
        
        //Tính tổng số lượng
        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<ItemCart> lsGioHang = Session["GioHang"] as List<ItemCart>;
            if(lsGioHang == null)
            {
                return 0;
            }
            return lsGioHang.Sum(n=>n.soLuong);
        }

        //Tính tổng tiền
        public decimal TinhTongtien()
        {
            //Lấy giỏ hàng
            List<ItemCart> lsGioHang = Session["GioHang"] as List<ItemCart>;
            if (lsGioHang == null)
            {
                return 0;
            }
            return lsGioHang.Sum(n => n.thanhTien);
        }

        public ActionResult XemGioHang()
        {
            //Lấy giỏ hàng
            List<ItemCart> lsGioHang = LayGioHang();
            return View(lsGioHang);
        }

        public ActionResult XemGioHangPartial()
        {
            if(TinhTongtien() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongtien();
            return PartialView();
        }

        //chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult SuaGioHang(int productID)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SanPham product = db.SanPham.SingleOrDefault(n => n.ProductID == productID);
            if (product == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemCart> lsGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng hay không
            ItemCart spCheck = lsGioHang.SingleOrDefault(n => n.productID == productID);
            if(spCheck == null)
            {
                return RedirectToAction("index", "Home");
            }
            //Lấy list giỏ hàng tạo giao diện
            ViewBag.GioHang = lsGioHang;

            //Nếu tồn tại rồi
            return View(spCheck);
        }

        //Xử lí cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemCart itemGH)
        {
            //Kiểm tra số lượng tồn 
            SanPham spCheck = db.SanPham.Single(n => n.ProductID == itemGH.productID);
            if(spCheck.SoLuongTon < itemGH.soLuong)
            {
                return View("ThongBao");
            }
            //Cập nhật số lượng trong item giỏ hàng
            //Bước 1: Lấy list<itemGioHang> từ session["GioHang"]
            List<ItemCart> lsGH = LayGioHang();
            //Lấy sản phẩm cập nhật từ List<ItemGioHang> ra
            ItemCart itemGHUpdate = lsGH.Find(n => n.productID == itemGH.productID);
            //Tiến hành cập nhật lại số lượng thành tiền
            itemGHUpdate.soLuong = itemGH.soLuong;
            itemGHUpdate.thanhTien = itemGHUpdate.soLuong * itemGHUpdate.Gia;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int productID)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            SanPham product = db.SanPham.SingleOrDefault(n => n.ProductID == productID);
            if (product == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemCart> lsGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng hay không
            ItemCart spCheck = lsGioHang.SingleOrDefault(n => n.productID == productID);
            if (spCheck == null)
            {
                return RedirectToAction("index", "Home");
            }
            //xóa item trong giỏ hàng
            lsGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang");
        }

        //Xây dựng chức năng đặt hàng
        public ActionResult DatHang(Customer kh)
        {
            //Kiểm tra xem giỏ hàng có tồn tại hay chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("index", "Home");
            }
            Users users = Session["userName"] as Users;
            Customer khach = new Customer();
            if (Session["userName"] == null)
            {
                //Thêm khách hàng vào bản khách hàng(khách vãng lai)
                return RedirectToAction("login", "Home");
            }
            else if (Session["userName"] != null)
            {

                //Với khách hàng là thành viên
                Users user = Session["userName"] as Users;
                khach = kh;
                khach.Email = user.email;
                khach.HoTen = users.HoTen;
                khach.userID = users.userID;
                db.Customer.Add(khach);
                db.SaveChanges();

            }
            //Thêm đơn hàng
            Order ddh = new Order();
            
            ddh.CustomerID = khach.CustomerID;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.Daduyet = false;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            
            db.Order.Add(ddh);
            db.SaveChanges();

            //Thêm chhi tiết đơn đặt hàng
            List<ItemCart> lsGH = LayGioHang();
            foreach(var item in lsGH)
            {
                OrderDetail cthd = new OrderDetail();
                cthd.OrderID = ddh.OrderID;
                cthd.ProductID = item.productID;
                cthd.ProductName = item.productName;
                cthd.soLuong = item.soLuong;
                cthd.Gia = item.Gia;
                db.OrderDetail.Add(cthd);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult ThemGioHangAjax(int productID, string strURL)
        {
            //Kiểm tr ẩn phẩm có tồn tại trong CSDL hay không 
            SanPham product = db.SanPham.SingleOrDefault(n => n.ProductID == productID);
            if (product == null)
            {
                //trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng
            List<ItemCart> lsGioHang = LayGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng
            ItemCart spCheck = lsGioHang.SingleOrDefault(n => n.productID == productID);
            if (spCheck != null)
            {
                //Kiểm tra số lượng tồn trướ khi cho khách hàng mua
                if (product.SoLuongTon < spCheck.soLuong)
                {
                    return Content("<script> alert(\"Sản phẩm hết hàng!\")</script>");
                }
                spCheck.soLuong++;
                spCheck.thanhTien = spCheck.soLuong * spCheck.Gia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongtien();
                return PartialView("XemGioHangPartial");
            }
            ItemCart itemGH = new ItemCart(productID);
            if (product.SoLuongTon < itemGH.soLuong)
            {
                return Content("<script> alert(\"Sản phẩm hết hàng!\")</script>");
            }
            lsGioHang.Add(itemGH);

            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongtien();
            return PartialView("XemGioHangPartial");
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