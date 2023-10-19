namespace Core.Interfaces;

public interface IProduct
{
    int Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    int Price { get; set; }
    double DiscountPercentage { get; set; }
    double Rating { get; set; }
    int Stock { get; set; }
    string Brand { get; set; }
    string Category { get; set; }
    string Thumbnail { get; set; }
    List<string> Images { get; set; }
}