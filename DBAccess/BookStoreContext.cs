using DBAccess.Model;
using DBAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DBAccess
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            :base(options)
        {

        }
        public DbSet<BookModel> books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=DESKTOP-NGIFUOT\SQLEXPRESS;Database=BookStoreToBeDel;" +
                "Integrated security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
