using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers;

[Route("order")]
[Authorize]

public class OrderController : Controller
{

}
