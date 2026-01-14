using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class ProductsDbContext : DbContext, IProductsDbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ProductSummary> Summaries { get; set; }
    public DbSet<ProductSummary> ProductSummaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Summary)
            .WithOne(s => s.Product) 
            .HasForeignKey<ProductSummary>(s => s.ProductId);

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.ReviewId);
            entity.Property(r => r.Title)
                .IsRequired();
            entity.Property(r => r.Body)
                .IsRequired();
            entity.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);
        });

        modelBuilder.Entity<ProductSummary>(entity =>
        {
            entity.ToTable("ProductSummaries");
            entity.HasKey(ps => ps.SummaryId);

            entity.HasOne(ps => ps.Product)
                .WithOne(ps => ps.Summary)
                .HasForeignKey<ProductSummary>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(ps => ps.Version)
                .HasDefaultValue(1);
        });
    }
}