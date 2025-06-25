using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public class VtuberMerchHubDbContext : DbContext
    {
        public VtuberMerchHubDbContext(DbContextOptions<VtuberMerchHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Vtuber> Vtubers { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).HasColumnName("role").IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
                entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            });

            // Cấu hình Admin
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("admin_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.AdminName).HasColumnName("admin_name").IsRequired().HasMaxLength(255);
                entity.HasOne(a => a.User)
                    .WithOne(u => u.Admin)
                    .HasForeignKey<Admin>(a => a.UserId);
            });

            // Cấu hình Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(255);
                entity.Property(e => e.Nickname).HasColumnName("nickname").HasMaxLength(255);
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
                entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                entity.Property(e => e.GenderId).HasColumnName("gender_id");
                entity.HasOne(c => c.User)
                    .WithOne(u => u.Customer)
                    .HasForeignKey<Customer>(c => c.UserId);
                entity.HasOne(c => c.Gender)
                    .WithMany(g => g.Customers)
                    .HasForeignKey(c => c.GenderId);
            });

            // Cấu hình Gender
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.GenderId).HasColumnName("gender_id");
                entity.Property(e => e.GenderType).HasColumnName("gender").IsRequired().HasMaxLength(50);
            });

            // Cấu hình Vtuber
            modelBuilder.Entity<Vtuber>(entity =>
            {
                entity.Property(e => e.VtuberId).HasColumnName("vtuber_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.VtuberName).HasColumnName("vtuber_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.RealName).HasColumnName("real_name").HasMaxLength(255);
                entity.Property(e => e.DebutDate).HasColumnName("debut_date");
                entity.Property(e => e.Channel).HasColumnName("channel").HasMaxLength(255);
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.VtuberGender).HasColumnName("vtuber_gender");
                entity.Property(e => e.SpeciesId).HasColumnName("species_id");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.ModelUrl).HasColumnName("model_url").HasMaxLength(255);
                entity.HasOne(v => v.User)
                    .WithOne(u => u.Vtuber)
                    .HasForeignKey<Vtuber>(v => v.UserId);
                entity.HasOne(v => v.Gender)
                    .WithMany(g => g.Vtubers)
                    .HasForeignKey(v => v.VtuberGender);
                entity.HasOne(v => v.Species)
                    .WithMany(s => s.Vtubers)
                    .HasForeignKey(v => v.SpeciesId);
                entity.HasOne(v => v.Company)
                    .WithMany(co => co.Vtubers)
                    .HasForeignKey(v => v.CompanyId);
            });

            // Cấu hình Species
            modelBuilder.Entity<Species>(entity =>
            {
                entity.Property(e => e.SpeciesId).HasColumnName("species_id");
                entity.Property(e => e.SpeciesName).HasColumnName("species_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasColumnName("description");
            });

            // Cấu hình Companies
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.CompanyName).HasColumnName("company_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(255);
                entity.Property(e => e.ContactEmail).HasColumnName("contact_email").HasMaxLength(255);
            });

            // Cấu hình Events
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("event_id");
                entity.Property(e => e.VtuberId).HasColumnName("vtuber_id");
                entity.Property(e => e.Date).HasColumnName("date").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description");
                entity.HasOne(e => e.Vtuber)
                    .WithMany(v => v.Events)
                    .HasForeignKey(e => e.VtuberId);
            });

            // Cấu hình Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.CategoryName).HasColumnName("category_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasColumnName("description");
            });

            // Cấu hình Merchandises
            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");
                entity.Property(e => e.VtuberId).HasColumnName("vtuber_id");
                entity.Property(e => e.MerchandiseName).HasColumnName("merchandise_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();
                entity.Property(e => e.EndDate).HasColumnName("end_date").IsRequired();
                entity.HasOne(m => m.Vtuber)
                    .WithMany(v => v.Merchandises)
                    .HasForeignKey(m => m.VtuberId);
            });

            // Cấu hình Products
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");
                entity.Property(e => e.ProductName).HasColumnName("product_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.HasOne(p => p.Merchandise)
                    .WithMany(m => m.Products)
                    .HasForeignKey(p => p.MerchandiseId);
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);
            });

            // Cấu hình Carts
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasOne(c => c.Customer)
                    .WithMany(cu => cu.Carts)
                    .HasForeignKey(c => c.CustomerId);
            });

            // Cấu hình Cart_Items
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
                entity.HasOne(ci => ci.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(ci => ci.CartId);
                entity.HasOne(ci => ci.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(ci => ci.ProductId);
            });

            // Cấu hình Orders
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.OrderDate).HasColumnName("order_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
                entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasDefaultValue(0); // 0: Pending, 1: Completed, 2: Cancelled
                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId);
            });

            // Cấu hình Order_Details
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
                entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(10,2)");
                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);
                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId);
            });
        }
    }
}