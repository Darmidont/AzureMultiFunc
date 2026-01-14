using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public sealed class DataService : IDataService
    {
        private const int MinimumReviewCount = 5;
        private readonly IProductsDbContext _productsDbContext;

        public DataService(IProductsDbContext productsDbContext)
        {
            _productsDbContext = productsDbContext;
        }

        public IList<Product> GetProducts()
        {
            var products = _productsDbContext.Products.AsNoTracking().Include(pr => pr.Reviews)
                .Include(_ => _.Summary)
                .Where(x => x.Reviews.Count >=MinimumReviewCount && x.Summary == null)
                .ToList();

            return products;
        }
    }
}
