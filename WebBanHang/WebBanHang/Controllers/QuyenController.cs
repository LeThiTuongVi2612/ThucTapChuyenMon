using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị")]
    public class QuyenController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang();
        // GET: Quyen
        public ActionResult Index()
        {
            var lstQuyen = db.Quyen.OrderBy(n => n.MaQuyen);
            return View(lstQuyen);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Quyen quyen)
        {
            db.Quyen.Add(quyen);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Quyen quyen = db.Quyen.SingleOrDefault(n => n.MaQuyen == id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Quyen quyen)
        {
            db.Quyen.Add(quyen);
            db.Entry(quyen).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Xoa(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Quyen quyen = db.Quyen.SingleOrDefault(n => n.MaQuyen == id);
            if (quyen == null)
            {
                return HttpNotFound();
            }
            return View(quyen);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XoaQuyen(string maQuyen)
        {
            if (maQuyen == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Quyen model = db.Quyen.SingleOrDefault(n => n.MaQuyen == maQuyen);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.Quyen.Remove(model);
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