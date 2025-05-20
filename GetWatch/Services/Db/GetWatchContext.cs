using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GetWatch.Services.Db;
using GetWatch.Services.Db.CartItem;
using GetWatch.Services.Db.Purchases;

namespace GetWatch.Services.Db
{
    public class GetWatchContext : DbContext
    {
        public DbSet<DbUser> DbUsers { get; set; }
        public DbSet<DbCart> DbCarts { get; set; }
        public DbSet<DbSupportTickets> DbSupportTickets { get; set; }
        public DbSet<DbPurchases> DbPurchases { get; set; }
        public DbSet<DbBluRayPurchase> DbBluRayPurchases { get; set; }
        public DbSet<DbTicketPurchase> DbTicketPurchases { get; set; }
        public DbSet<DbRentPurchase> DbRentPurchases { get; set; }
        
        public DbSet<DbCartItem> DbCartItem { get; set; }
        public DbSet<DbBluRayCart> BluRayCarts { get; set; }
        public DbSet<DbRentItem> RentItems { get; set; }
        public DbSet<DbTicketCart> TicketCarts { get; set; }

        public DbSet<DbCard> DbCards { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=GetWatch.db");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbUser>()
               .HasOne(u => u.Cart)
               .WithOne(c => c.User)
               .HasForeignKey<DbCart>(c => c.UserId);

            modelBuilder.Entity<DbUser>()
                .HasMany(u => u.SupportTickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<DbUser>()
                .HasMany(u => u.Transactions)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<DbUser>()
                .HasMany(u => u.Cards)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);     

            modelBuilder.Entity<DbCart>()
                .HasMany(c => c.CartItems)
                .WithOne(tp => tp.Cart)
                .HasForeignKey(tp => tp.CartId);

               
        }



        
    }
}
