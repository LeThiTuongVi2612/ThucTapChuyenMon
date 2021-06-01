using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(ProductMetadata))]
    public partial class Product
    {
        internal sealed class ProductMetadata
        {
            public int ProductID { get; set; }

            [Display(Name = "Tên sản phẩm:")]
            [Required(ErrorMessage ="{0} Tên sản phẩm không được để trống")]
            public string ProductName { get; set; }

            [Display(Name = "Số lượng:")]
            [Required(ErrorMessage = "{0} Số lượng không được để trống")]
            [Range(1,100000,ErrorMessage = "{0} phải từ {1} đến {2}")]
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "Hình ảnh:")]
            [Required(ErrorMessage = "{0} Hình ảnh không được để trống")]
            public string Anh { get; set; }

            [Display(Name = "Danh mục SP:")]
            public Nullable<int> GroupProductID { get; set; }

            [Display(Name = "Nhà cung cấp:")]
            public Nullable<int> CompanyID { get; set; }

            [Display(Name = "Mô tả:")]
            [Required(ErrorMessage = "{0} Mô tả không được để trống")]
            public string Mota { get; set; }

            [Display(Name = "Trạng thái:")]
            [Required(ErrorMessage = "{0} Trạng không được để trống")]
            public Nullable<bool> Status { get; set; }

            [Display(Name = "Giá:")]
            [Required(ErrorMessage = "{0} Giá không được để trống")]
            [Range(15000, 100000, ErrorMessage = "{0} phải từ {1} đến {2}")]
            public Nullable<decimal> Price { get; set; }

            [Display(Name = "Ngày cập nhật:")]
            [Required(ErrorMessage = "{0} Ngày cập nhật không được để trống")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
        }
    }
}