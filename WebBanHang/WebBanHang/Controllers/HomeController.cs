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
        public ActionResult DN()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult login(LoginModel login)
        //{
        //    var result = new AccountModel().Login(login.userName, login.passWord);
        //    if(result && ModelState.IsValid)
        //    {
        //        SessionHelper.SetSession(new UserSession() { userName = login.userName });
        //        return RedirectToAction("index", "Home");
        //    }
        //else
        //{
        //    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng!");

        //}
        //return View(login);
        //}
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sUserName = f["txtUserName"].ToString();
            string sPassWord = f["txtPassWord"].ToString();

            //Users user = db.Users.SingleOrDefault(n => n.userName == sUserName && n.passWord == sPassWord);
            //if (user != null)
            //{
            //    Session["userName"] = user;
            //    return RedirectToAction("index");
            //} 
            //return Content("Tài khoản hoặc mật khẩu không đúng");
            Users tv = db.Users.SingleOrDefault(n => n.userName == sUserName && n.passWord == sPassWord);
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
                PhanQuyen(tv.userName.ToString(), Quyen);
                Session["userName"] = tv;
                return RedirectToAction("index", "Home");
            }
            return Content("Tài khoản hoặc mật khẩu không đúng");
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
        public ActionResult ForGotPassWord()
        {
            return View();
        }
        public ActionResult PassWord()
        {
            return View();
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