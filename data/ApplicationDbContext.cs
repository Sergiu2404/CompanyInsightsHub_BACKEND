using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.data
{
    public class ApplicationDbContext : IdentityDbContext<User> 
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Stock> Stocks {get; set; }
        public DbSet<Comment> Comments {get; set; }

        public DbSet<Portfolio> Portfolios {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(entity => entity.HasKey(portfolio => new { portfolio.UserId, portfolio.StockId}));
            builder.Entity<Portfolio>().HasOne(user => user.User).WithMany(user => user.Portfolios).HasForeignKey(portfolio => portfolio.UserId);
            builder.Entity<Portfolio>().HasOne(stock => stock.Stock).WithMany(stock => stock.Portfolios).HasForeignKey(portfolio => portfolio.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}