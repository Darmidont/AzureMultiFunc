using BusinessLogic.Interfaces;
using Data.Entities;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class ProductProcessor: IProductProcessor
    {
        private readonly ILogger<ProductProcessor> _logger;

        private readonly IDataService _dataService;
        private readonly IAiReviewGenerator _generator;

        public ProductProcessor(ILogger<ProductProcessor> logger, IAiReviewGenerator generator, IDataService dataService)
        {
            _generator = generator;
            _dataService = dataService;
            _logger = logger;
        }

        public async Task<int> ProcessProductsAsync()
        {
            var products = await _dataService.GetProducts();
            if (!products.Any()) return 0;

            foreach (var product in products)
            {
                try
                {
                    var review = await _generator.GenerateReviewAsync(product);
                    ProductSummary summary = new ProductSummary
                    {
                        ProductId = product.ProductId,
                        SummaryText = review,
                        GeneratedAt = DateTime.UtcNow,
                        //ModelName = _generator.ModelName,
                    };

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error generating review for product {ProductId}", product.ProductId);
                }
            }

            return products.Count;
        }
    }
}
