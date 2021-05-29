namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int userID { get; set; }

        public string userName { get; set; }

        public string passWord { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public int? role { get; set; }

        public string email { get; set; }

        public string UserPic { get; set; }
    }
}
