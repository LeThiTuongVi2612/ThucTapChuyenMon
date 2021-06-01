﻿using System;
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
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy");
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.TenDanhMuc), "GroupProductID", "TenDanhMuc");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Product sp, HttpPostedFileBase[] Anh)
        {
            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy");
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.TenDanhMuc), "GroupProductID", "TenDanhMuc");
            //kiểm tra hình có tồn tại trong csdl chưa
            var fileName = Path.GetFileName(Anh[0].FileName);
            if (Anh[0].ContentLength > 0)
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
                    Anh[0].SaveAs(path);
                    sp.Anh = fileName;
                }
            }
            //sp.NgayCapNhat = DateTime.Now;
            db.Product.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Product sp = db.Product.SingleOrDefault(n => n.ProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy", sp.CompanyID);
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.TenDanhMuc), "GroupProductID", "TenDanhMuc", sp.GroupProductID);
            return View(sp);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Product model, HttpPostedFileBase[] Anh)
        {
            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy", model.CompanyID);
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.TenDanhMuc), "GroupProductID", "TenDanhMuc", model.GroupProductID);
            //kiểm tra hình có tồn tại trong csdl chưa
            var fileName = Path.GetFileName(Anh[0].FileName);
            if (Anh[0].ContentLength > 0)
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
                    Anh[0].SaveAs(path);
                    model.Anh = fileName;
                }
            }
            //sp.NgayCapNhat = DateTime.Now;
            db.Product.Add(model);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Admin");


        }

        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Product sp = db.Product.SingleOrDefault(n => n.ProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //Load dropdownlist nhà cung cấp
            ViewBag.CompanyID = new SelectList(db.Company.OrderBy(n => n.TenCongTy), "CompanyID", "TenCongTy", sp.CompanyID);
            ViewBag.GroupProductID = new SelectList(db.GroupProduct.OrderBy(n => n.TenDanhMuc), "GroupProductID", "TenDanhMuc", sp.GroupProductID);
            return View(sp);

        }


        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Product model = db.Product.SingleOrDefault(n => n.ProductID == id);
            if(model == null)
            {
                return HttpNotFound();
            }
            db.Product.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }
    }

    //https://www.tiny.cloud/docs/demo/basic-example/
}