using Data.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IAiReviewGenerator
    {
        Task<string?> GenerateReviewAsync(Product product);
    }
}
