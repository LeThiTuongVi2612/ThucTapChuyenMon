using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models
{
    public class AccountModels
    {
        private OnlineShopDBConnetion contexts = null;

        public AccountModels()
        {
            contexts = new OnlineShopDBConnetion();
        }

        public bool Login(string userName, string passWord)
        {
            object[] sqlParams =
            {
                new SqlParameter("@userName", userName),
                new SqlParameter("@passWord", passWord),

            };
            
            var res = contexts.Database.SqlQuery<bool>("Account_login @userName, @passWord", sqlParams).SingleOrDefault();
            return res;
        }
    }
}
