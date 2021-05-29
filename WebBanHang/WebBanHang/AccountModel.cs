using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBanHang.Models;

namespace WebBanHang
{
    public class AccountModel
    {
        private QuanLyBanHang context = null;

        public AccountModel()
        {
            context = new QuanLyBanHang();
        }

        public bool Login(string userName, string passWord)
        {
            object[] sqlParams =
            {
                new SqlParameter("@userName", userName),
                new SqlParameter("@passWord", passWord),

            };

            var res = context.Database.SqlQuery<bool>("Account_login @userName, @passWord", sqlParams).SingleOrDefault();
            return res;
        }
    
}
}