using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Pustok.ViewComponents;

public class ClientNavbarAlertMessagesViewComponent : ViewComponent
{
    private readonly ECommerceDBContext _eCommerceDbContext;
    private readonly IUserService _userService;

    public ClientNavbarAlertMessagesViewComponent(
        ECommerceDBContext eCommerceDbContext,
        IUserService userService)
    {
        _eCommerceDbContext = eCommerceDbContext;
        _userService = userService;
    }

    public IViewComponentResult Invoke()
    {
        if (!_userService.IsCurrentUserAuthenticated())
        {
            return View(new List<AlertMessageViewModel>());
        }

        var alertMessages = _eCommerceDbContext.AlertMessages
            .Where(am => am.UserId == _userService.CurrentUser.Id)
            .OrderByDescending(o => o.CreatedAt)
            .Select(am => new AlertMessageViewModel
            {
                Id = am.Id,
                Title = am.Title,
                Content = am.Content,
                CreatedAt = am.CreatedAt
            })
            .ToList();

        return View(alertMessages);
    }
}
