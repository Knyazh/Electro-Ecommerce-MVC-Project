using E_Commerce_Platform.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers;

[Route("products")]

public class ProductControlller : Controller
{
    private readonly ECommerceDBContext _dbContext;

}
