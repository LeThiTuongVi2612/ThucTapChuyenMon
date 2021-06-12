using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;

using WebBanHang.code;
using System.Web.Security;
using System.Net.Mail;
using System.Web.Helpers;
using System.Net;
using System.Security.Cryptography;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult OurCoffee()
        {
            var listSp = new QuanLyBanHang().GroupProduct.ToList();
            ViewBag.listGroup = listSp; 
            var listProduct1 = (from x in db.SanPham.Where(x=> x.Status==false) select x).ToList();
            ViewBag.ListSP = listProduct1;
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Users f)
        {
            //string sUserName = f["txtUserName"].ToString();
            //string sPassWord = f["txtPassWord"].ToString();
            string a = MaHoaMD5.MD5Hash(f.passWord);
            //Users user = db.Users.SingleOrDefault(n => n.userName == sUserName && n.passWord == sPassWord);
            //if (user != null)
            //{
            //    Session["userName"] = user;
            //    return RedirectToAction("index");
            //} 
            //return Content("Tài khoản hoặc mật khẩu không đúng");
            //Users tv = db.Users.SingleOrDefault(n => n.email == sUserName && n.passWord == a);
            var tv = (from c in db.Users where c.email == f.email && c.passWord == f.passWord select c).FirstOrDefault();
            if(tv != null)
            {
                
                //Truy cập lấy ra tất cả các quyền của thành viên đó
                var lstQuyen = db.LoaiTV_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                string Quyen = "";
                foreach(var item in lstQuyen)
                {
                    Quyen += item.Quyen.TenQuyen + ",";
                    //lấy quyền trong bảng chi tiết quyền và loại thành viên
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);//cắt đi dấu phẩy cuối cùng của chuỗi
                PhanQuyen(tv.email.ToString(), Quyen);
                Session["userName"] = tv;
                if (tv.MaLoaiTV == 1 || tv.MaLoaiTV == 2)
                {
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    return RedirectToAction("index", "Home");
                }
                
            }
            return Content("<script>alert(\"Tài khoản hoặc mật khẩu không đúng! Vui lòng load lại trang để đăng nhập!\")</script>");
        }

        public string GetMD5(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("X2");
            }

            return str_md5;
        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                TaiKhoan//users
                , DateTime.Now//begin
                , DateTime.Now.AddHours(1)//timeout
                , false//remember
                , Quyen, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }

        [HttpGet]
        public ActionResult Add_Account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Account(Users user)
        {
            //Kiểm tra captcha hợp lệ
            if(this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Thêm thành công";
                    
                    user.MaLoaiTV = 3;
                    db.Users.Add(user);
                    db.SaveChanges();
                    
                }
                else
                {
                    ViewBag.ThongBao = "Thêm thất bại";
                }
                return View();
            }
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }
        [HttpGet]
        public ActionResult ForGotPassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForGotPassWord(string email)
        {
            string message = "";
            bool status = false;
            var tv = db.Users.Where(n => n.email == email).FirstOrDefault();
            if(tv != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                 SendLinkEmail(tv.email, resetCode, "PassWord");
                tv.ResetPassWordCode = resetCode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "Bạn vui lòng check mail ấn vào link để tiếp tục!!! ";
            }
            else
            {
                message = "Acount not found";
            }
            ViewBag.ms = message;
            return View();
        }
        [NonAction]
        public void SendLinkEmail(string email, string actionvationCode, string EmailFor = "VerifyAccount")
        {
            string verifyUrl = "/Home/" + EmailFor + "/" + actionvationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var FromEmail = new MailAddress("CoffeeCafe2600@gmail.com", "CoffeCafe");
            var ToEmail = new MailAddress(email);
            var FromEmailPass = "lethituongvi00";
            string subject = "";
            string body = "";
            if (EmailFor == "VerifyAccount")
            {
                subject = "You account successfully created";
                body = "<br/><br/> We are excited to tell you that your CoffeeCafe account is " +
                    "successfully create. Please click on below link to verify your account " +
                    "<br/><br/><a href =" + link + ">" + link + "</a>";
            }
            else if (EmailFor == "PassWord")
            {
                subject = "ResetPassword";
                body = "Hi, <br/><br/>We got request for reset your account password. Please click on below link reset password!" +
                    "<br/><br/>< a href =" + link +" >  Reset Password  </ a > ";
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials =false,
                Credentials = new NetworkCredential(FromEmail.Address, FromEmailPass)
            };
            using (var message = new MailMessage(FromEmail, ToEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
            smtp.Send(message);
        }

        

        [HttpGet]
        public ActionResult PassWord(string id)
        {
            var user = db.Users.Where(n => n.ResetPassWordCode == id).FirstOrDefault();
            if(user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PassWord(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
               var user = db.Users.Where(n => n.ResetPassWordCode == model.ResetCode).FirstOrDefault();
                if(user != null)
                {
                    
                    user.passWord = model.NewPassWord;
                    user.ResetPassWordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Cập nhật password thành công";
                }
                
            }
            else
            {
                message = "Không hợp lệ";
            }
            ViewBag.Message = message;
            return View(model);
        }
            public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Partial()
        {
            return View();
        }
        public ActionResult MenuPartial()
        {
            
            return View();
        }
        public ActionResult GroupProduct()
        {
            var listSp = new QuanLyBanHang().GroupProduct.ToList();
            ViewBag.listGroup = listSp;
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["userName"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }

        public ActionResult LoiPhanQuyen()
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