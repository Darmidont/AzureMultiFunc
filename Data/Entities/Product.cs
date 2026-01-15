namespace Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ProductSummary? Summary { get; set; } = null!;
    }
}
