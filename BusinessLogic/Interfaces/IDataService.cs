using Data.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IDataService
    {
        Task<IList<Product>> GetProducts();
    }
}
