using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataTypeAttribute(typeof(UsersMetadata))]
    public partial class Users
    {
        internal sealed class UsersMetadata
        {
            public int userID { get; set; }
            public string passWord { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string HoTen { get; set; }
            public Nullable<int> MaLoaiTV { get; set; }
            [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Please enter correct email address")]
            public string email { get; set; }
            public string ResetPassWordCode { get; set; }
        }
    }
}