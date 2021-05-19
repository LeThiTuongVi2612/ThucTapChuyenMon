using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class CustomerController : Controller
    {
        QuanLyBanHangOnlEntities db = new QuanLyBanHangOnlEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}