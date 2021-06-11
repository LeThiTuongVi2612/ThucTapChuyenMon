using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;


namespace WebBanHang.Controllers
{
    [Authorize(Roles = "Quản trị")]
    public class PhanQuyenController : Controller
    {
        QuanLyBanHang db = new QuanLyBanHang()
;        // GET: PhanQuyen
        public ActionResult Index()
        {
            var lstLoaiTV = db.LoaiThanhVien.OrderBy(n => n.TenLoaiTV);
            return View(lstLoaiTV);
        }

        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(LoaiThanhVien tv)
        {
            //sp.NgayCapNhat = DateTime.Now;
            db.LoaiThanhVien.Add(tv);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            LoaiThanhVien sp = db.LoaiThanhVien.SingleOrDefault(n => n.MaLoaiTV == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(LoaiThanhVien loai)
        {
            db.LoaiThanhVien.Add(loai);
            db.Entry(loai).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(int? id)
        {
            //lấy sản phẩm chỉnh sửa dựa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiThanhVien loaiTV = db.LoaiThanhVien.SingleOrDefault(n => n.MaLoaiTV == id);
            if (loaiTV == null)
            {
                return HttpNotFound();
            }
            return View(loaiTV);
        }

        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            LoaiThanhVien model = db.LoaiThanhVien.SingleOrDefault(n => n.MaLoaiTV== id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.LoaiThanhVien.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PhanQuyen(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiThanhVien loai = db.LoaiThanhVien.SingleOrDefault(n => n.MaLoaiTV == id);
            if(loai == null)
            {
                return HttpNotFound();
            }
            //lấy ra danh sách quyền
            ViewBag.Quyens = db.Quyen;
            //lấy ra danh sách loại thành viên của thành viên đó
            ViewBag.loaiTVQuyen = db.LoaiTV_Quyen.Where(n => n.MaLoaiTV == id);
            return View(loai);
        }
        [HttpPost]
        public ActionResult PhanQuyens(int? MaLoaiTV, IEnumerable<LoaiTV_Quyen> loaiTVQ)
        {
            //Trường hợp: nếu đã phân quyền rồi nhưng muốn phân quyền lại
            //1. Xóa những quyền cũ thuộc loại thành viên đó
            var lstDaPhanQuyen = db.LoaiTV_Quyen.Where(n => n.MaLoaiTV == MaLoaiTV);
            if(lstDaPhanQuyen != null)
            {
                db.LoaiTV_Quyen.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }
            //kiểm tra list danh sách quyền được check
            foreach(var item in loaiTVQ)
            {
                item.MaLoaiTV = int.Parse(MaLoaiTV.ToString());
                db.LoaiTV_Quyen.Add(item);
            }
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