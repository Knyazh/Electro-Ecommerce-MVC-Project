using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Services.Abstracts;

namespace E_Commerce_Platform.Services.Concretes;

public class UserActivationService : IUserActivationService
{

    private readonly IEmailService _emailService;
    private readonly ECommerceDBContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public UserActivationService(IEmailService emailService, ECommerceDBContext dbContext, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
    {
        _emailService = emailService;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }


    public void CreateAndSendActivationtoken( User user)
    {
        var activation = new UserActivation
        {
            Token = Guid.NewGuid(),
            User = user,
            ExpireDate = DateTime.Now.AddHours(2)
        };

        var activationRoute= _linkGenerator.GetPathByRouteValues
            (_httpContextAccessor.HttpContext,"Please Register Account Verification",new {activation.Token});

        _dbContext.UserActivations.Add(activation);

       

      _emailService.SendEmail("Account Activation", activationRoute, user.Email);


    }
}
