using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using System.Net.Mail;

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
            var lstDSDHCG = db.Order.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == true).OrderBy(n => n.NgayGiao);
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
        public ActionResult DuyetDonHang(Order ddh)
        {
            //truy vấn lấy dữ liệu của đơn đặt hàng đó
            Order ddhUpdate = db.Order.Single(n => n.OrderID == ddh.OrderID);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            db.SaveChanges();

            //Lấy chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.OrderDetail.Where(n => n.OrderID == ddh.OrderID);
            ViewBag.ListCTHD = lstChiTietDH;

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