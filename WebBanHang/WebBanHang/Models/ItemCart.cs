using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class ItemCart
    {
        public int productID { get; set; }

        public string productName { get; set; }
        public int soLuong { get; set; }
        public decimal Gia { get; set; }
        public string anh { get; set; }
        public decimal thanhTien { get; set; }

        public ItemCart(int iProductID)
        {
            using(QuanLyBanHang db = new QuanLyBanHang())
            {
                this.productID = iProductID;
                Product product = db.Product.Single(n => n.ProductID == iProductID);
                this.productName = product.ProductName;
                this.anh = product.Anh;
                this.Gia = product.Price.Value;
                this.soLuong = 1;
                this.thanhTien = soLuong * Gia;
            }
        }
        public ItemCart(int iProductID, int sl)
        {
            using (QuanLyBanHang db = new QuanLyBanHang ())
            {
                this.productID = iProductID;
                Product product = db.Product.Single(n => n.ProductID == iProductID);
                this.productName = product.ProductName;
                this.anh = product.Anh;
                this.Gia = product.Price.Value;
                this.soLuong = sl;
                this.thanhTien = soLuong * Gia;
            }
        }
        public ItemCart()
        {

        }
    }
}