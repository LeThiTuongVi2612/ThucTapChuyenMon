using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using System.Net.Mail;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý đơn hàng")]
    public class QuanLyDonHangController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: QuanLyDonHang
        public ActionResult ChuaThanhToan()
        {
            var lstChuaThanhToan = db.Order.Where(n => n.Daduyet==false && n.TinhTrangGiaoHang == false).OrderBy(n => n.NgayDat);
            return View(lstChuaThanhToan);
        }
        public ActionResult DaDuyet()
        {
            var lstChuaThanhToan = db.Order.Where(n => n.Daduyet == true && n.TinhTrangGiaoHang == false).OrderBy(n => n.NgayDat);
            return View(lstChuaThanhToan);
        }
        public ActionResult ChuaGiao()
        {
            var lstDSDHCG = db.Order.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == true && n.Daduyet == true).OrderBy(n => n.NgayGiao);
            return View(lstDSDHCG);
        }

        public ActionResult DaGiaoDaThanhToan()
        {
            var lstDGDTT = db.Order.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true&& n.Daduyet ==true);
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
            if (model == null)
            {
                return HttpNotFound();
            }


            //Lấy chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.OrderDetail.Where(n => n.OrderID == id);
            ViewBag.ListCTHD = lstChiTietDH;
            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(Order ddh, string gmail, string tenKH, string sdt, string diachi, string madonhang, string ngaydat, string duyetdon, string thanhtoan, string giaohang, string tenSP, string soluong, string gia, string tongsoluong, string tongtien)
        {
            //truy vấn lấy dữ liệu của đơn đặt hàng đó
            Order ddhUpdate = db.Order.Single(n => n.OrderID == ddh.OrderID);
            ddhUpdate.Daduyet = ddh.Daduyet;
            duyetdon = ddhUpdate.Daduyet.Value.ToString();
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            thanhtoan = ddhUpdate.DaThanhToan.Value.ToString();
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            giaohang = ddhUpdate.TinhTrangGiaoHang.Value.ToString();
            db.SaveChanges();

            //Lấy chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.OrderDetail.Where(n => n.OrderID == ddh.OrderID);
            ViewBag.ListCTHD = lstChiTietDH;

           
            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/Template/mail.html"));
            content = content.Replace("{{HoTen}}", tenKH);
            content = content.Replace("{{DiaChi}}", diachi);
            content = content.Replace("{{orderID}}", madonhang);
            content = content.Replace("{{NgayDat}}", ngaydat);
            content = content.Replace("{{Daduyet}}", duyetdon);
            content = content.Replace("{{DaThanhToan}}", thanhtoan);
            content = content.Replace("{{TinhTrangGiaoHang}}", giaohang);
            content = content.Replace("{{ProductName}}", tenSP);
            content = content.Replace("{{SoLuong}}", soluong);
            content = content.Replace("{{Gia}}", gia);
            content = content.Replace("{{TognSoLuong}}", tongsoluong);
            content = content.Replace("{{TongTien}}", tongtien);
            GuiEmail("Xác nhận đơn hàng của hệ thống", gmail, "CoffeeCafe2600@gmail.com", "lethituongvi00", content);
            return View(ddhUpdate);
        }
        
        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            //gởi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail);//Địa chỉ nhận
            mail.From = new MailAddress(ToEmail);//Địa chỉ gửi
            mail.Subject = Title;//tiêu đề gửi
            mail.Body = Content;//nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";//host gửi của email
            smtp.Port = 587;//port của địa chỉ gửi
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(FromEmail, PassWord);//tài khoản, mật khẩu người gửi
            smtp.EnableSsl = true;//kích hoạt giao tiếp an toàn SSl;
            smtp.Send(mail);//Gửi mail đi
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