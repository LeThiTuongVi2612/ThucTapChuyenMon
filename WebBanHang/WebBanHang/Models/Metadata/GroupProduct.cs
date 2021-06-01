using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(GroupProductMetadata))]
    public partial class GroupProduct
    {

        internal sealed class GroupProductMetadata
        {
            public int GroupProductID { get; set; }
            [Display(Name = "Tên danh mục:")]
            [Required(ErrorMessage = "{0} Tên danh mục không được để trống")]
            public string TenDanhMuc { get; set; }

            [Display(Name = "Hình ảnh:")]
            [Required(ErrorMessage = "{0} Hình ảnh không được để trống")]
            public string Pic { get; set; }

            [Display(Name = "Trạng thái:")]
            [Required(ErrorMessage = "{0} Trạng thái không được để trống")]
            public Nullable<bool> Status { get; set; }

            [Display(Name = "Ngày cập nhật:")]
            [Required(ErrorMessage = "{0} Ngày cập nhật không được để trống")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
        }
    }
}