using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class AdminController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: Admin
        public ActionResult Admin()
        {
            return View(db.Product.Where(n => n.Status == false).OrderByDescending(n => n.ProductID));
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {

            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.CompanyName), "CompanyID", "CompanyName");
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.GroupProductName), "GroupProductID", "GroupProductName");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Product sp, HttpPostedFileBase Anh)
        {
            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.CompanyName), "CompanyID", "CompanyName");
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.GroupProductName), "GroupProductID", "GroupProductName");
            //kiểm tra hình có tồn tại trong csdl chưa
            var fileName = Path.GetFileName(Anh.FileName);
            if(Anh.ContentLength > 0)
            {
                //lấy hình ảnh vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                // nếu thư mục chứa hình ảnh rồi thì xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    //lấy hình ảnh vào thư mục hình ảnh
                    Anh.SaveAs(path);
                    sp.Anh = fileName;
                }
            }
            db.Product.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
    }
}