using E_Commerce_Platform.Database.Base;

namespace E_Commerce_Platform.DataBase.Models;

public class AlertMessage : BaseEntity<int>, IAuditable
{
    public string Title { get; set; }
    public string Content { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
