namespace Core.Entities;

public class CartItem
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required string PictureUrl { get; set; }
    public required string Brand { get; set; }
    public required string Type { get; set; }
    public CartItem()
    {
        
    }
    public CartItem(
        int productId, 
        int quantity, 
        decimal price,
        string productName,
        string pictureUrl,
        string brand,
        string type
    )
    {
        ProductId = productId;
        Quantity = quantity; 
        Price = price;
        ProductName = productName;
        PictureUrl = pictureUrl;
        Brand = brand;
        Type = type;
    }
}
