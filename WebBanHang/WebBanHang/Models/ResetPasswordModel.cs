using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class ResetPasswordModel
    {   [Display(Name ="Mật khẩu mới")]
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassWord { get; set; }

        [Display(Name ="Nhập lại mật khẩu mới")]
        [DataType(DataType.Password)]
        [Compare("NewPassWord", ErrorMessage ="Mật khẩu mới và nhập lại mật khẩu mới không giống nhau!")]
        public string ConfirPassWord { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}