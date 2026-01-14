namespace Data.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
