using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(CompanyMetadata))]
    public partial class Company
    {
        internal sealed class CompanyMetadata
        {
            public int CompanyID { get; set; }

            [Display(Name = "Tên nhà cung cấp:")]
            [Required(ErrorMessage = "{0} Tên nhà cung cấp không được để trống")]
            public string TenCongTy { get; set; }

            [Display(Name = "Địa chỉ:")]
            [Required(ErrorMessage = "{0} Địa chỉ không được để trống")]
            public string diachi { get; set; }

            [Display(Name = "Số điện thoại:")]
            [StringLength(10, ErrorMessage ="{0} Không quá 10 kí tự")]
            
            public string SDT { get; set; }

           
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [Display(Name = "Trạng thái:")]
            [Required(ErrorMessage = "{0} Trạng thái không được để trống")]
            public Nullable<bool> Status { get; set; }
        }
    }
}