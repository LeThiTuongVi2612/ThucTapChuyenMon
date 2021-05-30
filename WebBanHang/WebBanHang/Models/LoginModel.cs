using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class LoginModel
    { 
        [Required]
        public string userName
        { set; get; }

        public string passWord { set; get; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string email { get; set; }
    }
}