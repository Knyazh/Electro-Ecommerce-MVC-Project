namespace E_Commerce_Platform.ViewModels.User;

public class ProductDetailsViewModel
{

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<string> Categories { get; set; }
    public string ImageUrl { get; set; }
}
