﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        QuanLyBanHangOnlEntities db = new QuanLyBanHangOnlEntities();
        // GET: Product

        public ActionResult Breakfast()
        {
            var listBreafast = db.Product.Where(x => x.GroupProductID == 2);
            ViewBag.ListAS = listBreafast;


            return View();
        }

        public ActionResult Lunch()
        {
            var listLuch = db.Product.Where(x => x.GroupProductID == 3);
            ViewBag.ListAT = listLuch;


            return View();
        }
        public ActionResult Product1()
        {
            

            var listProduct1 = db.Product.Where(x => x.GroupProductID == 1);
            ViewBag.ListSP = listProduct1;

            return View();
        }
        public ActionResult ProductPartial()
        {
            
            return ProductPartial();
        }
        
    }
}