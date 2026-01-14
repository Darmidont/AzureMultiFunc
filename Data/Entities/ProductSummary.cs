namespace Data.Entities;

public class ProductSummary
{
    public int SummaryId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string SummaryText { get; set; } = null!;
    public DateTime GeneratedAt { get; set; }
    public string? ModelName { get; set; }
    public string? PromptHash { get; set; }
    public int Version { get; set; } = 1;
}