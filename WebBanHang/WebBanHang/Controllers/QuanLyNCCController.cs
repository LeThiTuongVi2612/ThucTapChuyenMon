using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị")]
    public class QuanLyNCCController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: QuanLyNCC
        public ActionResult Index()
        {
            return View(db.Company.Where(n => n.Status == false).OrderByDescending(n => n.CompanyID));
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Company sp)
        {


            sp.NgayCapNhat = DateTime.Now;
            db.Company.Add(sp);

           
                db.SaveChanges();
            

            return RedirectToAction("index");
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
            Company sp = db.Company.SingleOrDefault(n => n.CompanyID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }


            return View(sp);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Company model)
        {
            model.NgayCapNhat = DateTime.Now;
            db.Company.Add(model);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            Company sp = db.Company.SingleOrDefault(n => n.CompanyID == id);
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
            Company model = db.Company.SingleOrDefault(n => n.CompanyID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Company.Remove(model);
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