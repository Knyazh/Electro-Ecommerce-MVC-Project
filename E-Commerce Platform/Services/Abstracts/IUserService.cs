using E_Commerce_Platform.DataBase.Models;
using System.Security.Claims;

namespace E_Commerce_Platform.Services.Abstracts
{
    public interface IUserService
    {
        bool IsCurrentUserAuthenticated();
        public User CurrentUser { get; }

        string GetCurrentUserFullName();

        List<Claim> GetClaimsAccordingToRole(User user);

        string GetUserFullName(User user);

        //List<User> GetAllStaffMembers();
    }
}
