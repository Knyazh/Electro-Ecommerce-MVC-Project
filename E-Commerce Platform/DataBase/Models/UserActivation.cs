using E_Commerce_Platform.Database.Base;

namespace E_Commerce_Platform.DataBase.Models;

public class UserActivation : BaseEntity<decimal>, IAuditable
{

    public Guid Token { get; set; }
    public DateTime ExpireDate { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set ; }
    public DateTime UpdatedAt { get; set; }
}
