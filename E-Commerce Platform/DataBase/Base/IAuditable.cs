namespace E_Commerce_Platform.Database.Base;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
