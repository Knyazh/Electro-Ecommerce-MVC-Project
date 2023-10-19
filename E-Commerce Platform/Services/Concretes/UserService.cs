using E_Commerce_Platform.Contracts;
using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Services.Abstracts;
using System.Data;
using System.Security.Claims;

namespace E_Commerce_Platform.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ECommerceDBContext _eCommerceDBContext;
        private User _currentUser;
        public UserService(IHttpContextAccessor httpContextAccessor, ECommerceDBContext eCommerceDBContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _eCommerceDBContext = eCommerceDBContext;
        }


        public User currentUser
        { 
            get 
            { 
                if (currentUser != null)
                {
                    return currentUser;
                }

                if(_httpContextAccessor.HttpContext.User == null)
                {
                    throw new Exception("User is not authenticated");
                }

                var userIdCaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

                if (userIdCaim is null)
                {
                    throw new Exception("User is not authenticated");
                }

                var userId=Convert.ToInt32(userIdCaim.Value);
                var user = _eCommerceDBContext.Users.SingleOrDefault(u => u.Id == userId);
                if (user is null)
                {
                    throw new Exception("User cant found");
                }

                _currentUser = user;
                return _currentUser;
            }
        }



        public User CurrentUser => throw new NotImplementedException();

        public List<User> GetAllStaffMembers()
        {
            throw new NotImplementedException();
        }

        public List<Claim> GetClaimsAccordingToRole(User user)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserFullName()
        {
            return $"{_currentUser.Name} {_currentUser.LastName}";
        }

        public string GetUserFullName(User user)
        {
            return $"{user.Name} {user.LastName}";
        }


        public bool DoesUserHaveRole(User user, Role.Values role)
        {
            return user.Role == role;
        }

        public bool IsCurrentUserAuthenticated()
        {
            return  _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }



    }
}
