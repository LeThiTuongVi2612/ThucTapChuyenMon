using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.Framework
{
    public partial class OnlineShopDBConnetion : DbContext
    {
        public OnlineShopDBConnetion()
            : base("name=OnlineShopDBConnetion")
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(e => e.Phone)
                .IsFixedLength();
        }
    }
}
