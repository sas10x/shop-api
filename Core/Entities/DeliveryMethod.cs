namespace Core.Entities;

public class DeliveryMethod : BaseEntity
{
    public DeliveryMethod() { }
    public DeliveryMethod(string shortName, string deliveryTime, string description, decimal price)
    {
        ShortName = shortName;
        DeliveryTime = deliveryTime;
        Description = description;
        Price = price;
    }
    public required string ShortName { get; set; }
    public required string DeliveryTime { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}
