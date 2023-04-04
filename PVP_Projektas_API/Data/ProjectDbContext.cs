using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Data;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> DbUsers { get; set; } = null!;
    public DbSet<Product> DbProducts { get; set; } = null!;
    public DbSet<Category> DbCategories { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductCategory)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryName)
            .IsRequired(false);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductShelf)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.ShelfId)
            .IsRequired(false);
    }
    public DbSet<Trade> DbTrades { get; set; } = null!;
    public DbSet<GiveawaySpot> DbGiveawaySpots { get; set; }
    public DbSet<Shelf> DbShelves { get; set; } = null!;
    public DbSet<Admin> DbAdmins { get; set; } = null!;
    public DbSet<Address> DbAddresses { get; set; } = null!;

}