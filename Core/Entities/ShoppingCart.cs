namespace Core.Entities;

public class ShoppingCart
{        
    public required string Id { get; set; }
    public List<CartItem> Items { get; set; } = [];
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public AppCoupon? Coupon { get; set; }
    public ShoppingCart()
    {
        
    }
    public ShoppingCart(string id, List<CartItem> items, int deliveryMethodId)
    {
        Id = id;
        Items = items;
        DeliveryMethodId = deliveryMethodId;
    }
}
