using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IProductsDbContext
    {
        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<ProductSummary> Summaries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

