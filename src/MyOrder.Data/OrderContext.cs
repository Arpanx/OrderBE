using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOrder.Model;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyOrder.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Orders> Orders { get; set; }        

        public OrderContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<Item>()
                .ToTable("Item");

            modelBuilder.Entity<Item>()
                .Property(s => s.CreatorId)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(s => s.DateCreated)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Item>()
                .Property(s => s.DateUpdated)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Item>()
                .Property(s => s.Type)
                .HasDefaultValue(OrderType.Retail);

            modelBuilder.Entity<Item>()
                .Property(s => s.Status)
                .HasDefaultValue(OrderStatus.Valid);

            modelBuilder.Entity<Item>()
                .HasOne(s => s.Creator)
                .WithMany(c => c.ItemsCreated);

            modelBuilder.Entity<Orders>()
              .ToTable("Orders");

            modelBuilder.Entity<Orders>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
