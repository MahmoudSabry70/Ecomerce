using Ecomerce_test1.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_test1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApllecationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<ApllecationUser> ApllecationUsers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
   
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }

        public virtual DbSet<ShoppingSession> ShoppingSessions { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProductId });

                entity.Property(e => e.CategoryId);

                entity.Property(e => e.ProductId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.User).WithMany()
                    .HasForeignKey(o => o.UserId).IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(o => o.OrderId).IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.Address).WithMany((string)null)
                    .HasForeignKey(o => o.AddressId).IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(oi => oi.User)
                    .WithMany((string)null).HasForeignKey(oi => oi.UserId)
                    .IsRequired(false).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId).IsRequired(true)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            //I am not using it for the moment I really have my doubts on how to manage Rating / Comment / Replies feature.


          


           

           
        }
    }









}

