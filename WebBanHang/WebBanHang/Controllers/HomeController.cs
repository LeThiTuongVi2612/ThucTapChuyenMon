using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;

using WebBanHang.code;

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

            Users user = db.Users.SingleOrDefault(n => n.userName == sUserName && n.passWord == sPassWord);
            if (user != null)
            {
                Session["userName"] = user;
                return RedirectToAction("index", "Home");
            }
            
            
            
            
            return Content("Tài khoản hoặc mật khẩu không đúng");
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
            return RedirectToAction("index");
        }



    }

}