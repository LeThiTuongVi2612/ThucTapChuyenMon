//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanHang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonDangHang
    {
        public int MaChiTietPN { get; set; }
        public Nullable<int> MaPN { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<decimal> DonGiaNhap { get; set; }
        public Nullable<int> SoLuongNhap { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}