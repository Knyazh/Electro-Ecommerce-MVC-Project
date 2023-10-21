using E_Commerce_Platform.Database.Base;

namespace E_Commerce_Platform.DataBase.Models;

public class OrderItem : BaseEntity<decimal>, IAuditable
{

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public string ProductName { get; set; }

    public decimal ProductPrice { get; set; }

    public string ProductOrderPhoto { get; set; }

    public int ProductQuantity { get; set; }

    public string ProductDescription { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get ; set; }
}
