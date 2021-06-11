using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị, Quản lý danh mục sản phẩm")]
    public class QuanLyDanhMucSanPhamController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: QuanLyDanhMucSanPham
        [Authorize(Roles = "Quản trị, Quản lý danh mục sản phẩm")]
        public ActionResult Index()
        {
            return View(db.GroupProduct.Where(n => n.Status == false).OrderByDescending(n => n.GroupProductID));
        }
        [Authorize(Roles = "Quản trị, Quản lý danh mục sản phẩm")]
        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(GroupProduct sp, HttpPostedFileBase[] Pic)
        {
            
            //kiểm tra hình có tồn tại trong csdl chưa
            var fileName = Path.GetFileName(Pic[0].FileName);
            if (Pic[0].ContentLength > 0)
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
                    Pic[0].SaveAs(path);
                    sp.Pic = fileName;
                }
            }
            sp.NgayCapNhat = DateTime.Now;
            db.GroupProduct.Add(sp);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [Authorize(Roles = "Quản trị")]
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            GroupProduct sp = db.GroupProduct.SingleOrDefault(n => n.GroupProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            
            return View(sp);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(GroupProduct model, HttpPostedFileBase[] Pic)
        {

            //kiểm tra hình có tồn tại trong csdl chưa
            var a = Path.GetFileName(Pic[0].FileName);
            if (Pic[0].ContentLength > 0)
            {
                //lấy hình ảnh vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Content/images"), a);
                // nếu thư mục chứa hình ảnh rồi thì xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    //lấy hình ảnh vào thư mục hình ảnh
                    Pic[0].SaveAs(path);
                    model.Pic = a;
                }
            }
            //sp.NgayCapNhat = DateTime.Now;
            db.GroupProduct.Add(model);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "Quản trị")]
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            GroupProduct sp = db.GroupProduct.SingleOrDefault(n => n.GroupProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            
            return View(sp);

        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            GroupProduct model = db.GroupProduct.SingleOrDefault(n => n.GroupProductID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.GroupProduct.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
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