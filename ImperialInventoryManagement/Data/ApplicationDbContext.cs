using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;


namespace ImperialInventoryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                _config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create identity models
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.InventoryItem)
                .WithMany()
                .HasForeignKey(x => x.InventoryItemId);

            modelBuilder.Entity<Shipment>()
                .HasOne(x => x.Order)
                .WithMany()
                .HasForeignKey(x => x.OrderId);
          

            modelBuilder.Entity<InventoryItem>()
                .HasOne(x => x.Facility)
                .WithMany(x => x.InventoryItems)
                .HasForeignKey(x => x.FacilityId);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId);

            modelBuilder.Entity<ItemCategory>()
                .HasKey(x => new { x.ItemId, x.CategoryId });
            modelBuilder.Entity<ItemCategory>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ItemCategories)
                .HasForeignKey(x  => x.ItemId);

            modelBuilder.Entity<ItemCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ItemCategories)
                .HasForeignKey(x => x.CategoryId);

            /*modelBuilder.Entity<ItemCategory>()
                .HasKey(x => new {x.CategoryId, x.ItemId});*/
        }



    }
}
