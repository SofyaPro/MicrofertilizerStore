using MicrofertilizerStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicrofertilizerStore.DataAccess
{
    public class MicrofertilizerStoreDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<DiscountEntity> Discounts { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }

        public MicrofertilizerStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<UserEntity>().HasIndex(x => x.INN).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.PhoneNumber).IsUnique();

            modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AdminEntity>().HasIndex(x => x.Login).IsUnique();

            modelBuilder.Entity<DiscountEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<DiscountEntity>().HasIndex(x => x.StartDate).IsUnique();
            modelBuilder.Entity<DiscountEntity>().HasIndex(x => x.EndDate).IsUnique();

            modelBuilder.Entity<ProductEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<ProductEntity>().HasOne(x => x.Discount)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.DiscountId);

            modelBuilder.Entity<CartEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<CartEntity>().HasIndex(x => new { x.UserId, x.ProductId}).IsUnique();
            modelBuilder.Entity<CartEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<CartEntity>().HasOne(x => x.Product)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<CartEntity>().HasOne(x => x.User)
               .WithMany(x => x.Carts)
               .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<OrderEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<OrderEntity>().HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<OrderDetailsEntity>().HasKey(x => x.Id); 
            modelBuilder.Entity<OrderDetailsEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<OrderDetailsEntity>().HasIndex(x => new { x.ProductId, x.OrderId }).IsUnique();
            modelBuilder.Entity<OrderDetailsEntity>().HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<OrderDetailsEntity>().HasOne(x => x.Order)
               .WithMany(x => x.OrderDetails)
               .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<FeedbackEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<FeedbackEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<FeedbackEntity>().HasOne(x => x.User)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.UserId);
        }
    }
}