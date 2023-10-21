using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.Database.Base;

namespace E_Commerce_Platform.DataBase.Models;

public class Order : BaseEntity<decimal>, IAuditable
{
    public int UserId { get; set; }

    public User User { get; set; }

    public string TrackingCode { get; set; }

    public Orderstatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get ; set ; }

    public List<OrderItem> OrderItems { get; set; }
}
