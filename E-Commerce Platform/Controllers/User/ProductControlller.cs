using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers.User;

[Route("products")]

public class ProductControlller : Controller
{
    private readonly ECommerceDBContext _dbContext;
    private readonly IFileService _fileService;

    public ProductControlller(ECommerceDBContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    [HttpGet("{id}/details")]
    public IActionResult GetDetails(int id)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

        if (product is null)
        {
            return RedirectToAction("ErrorPage", "Auth");
        }

        var model = new ProductDetailsViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,

            Categories = _dbContext.CategoryProducts
                .Where(cp => cp.ProductId == product.Id)
                .Select(cp => cp.Category.Name)
                .ToList(),
            ImageUrl = _fileService
                 .GetStaticFilesUrl(CustomUploadDirectories.Products, product.PhysicalImageName)


        };

        return Json(model);
    }

}
