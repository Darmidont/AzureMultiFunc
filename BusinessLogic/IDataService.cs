using Data.Entities;

namespace BusinessLogic
{
    public interface IDataService
    {
        IList<Product> GetProducts();
    }
}
