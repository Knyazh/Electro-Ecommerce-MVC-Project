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


        public User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                if (_httpContextAccessor.HttpContext.User == null)
                {
                    throw new Exception("User is not authenticated");
                }

                var userIdCaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")
                    ?? throw new Exception("User is not authenticated");



                var userId = Convert.ToInt32(userIdCaim.Value);
                var user = _eCommerceDBContext.Users.SingleOrDefault(u => u.Id == userId)
                    ?? throw new Exception("User cant found");

                _currentUser = user;
                return _currentUser;
            }
        }


        //public List<User> GetAllStaffMembers()
        //{
        //    var staffMembers = _eCommerceDBContext.Users.Where(sm =>
        //  sm.Role == Role.Values.Admin ||
        //  sm.Role == Role.Values.Moderator ||
        //  sm.Role == Role.Values.SuperAdmin)
        //      .ToList();

        //    return staffMembers;
        //}

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


        //public bool DoesUserHaveRole(User user, Role.Values role)
        //{
        //    return user.Role == role;
        //}

        public bool IsCurrentUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        //public List<Claim> GetClaimsAccortingToRole(User user)
        //{
        //    var claims = new List<Claim>();

        //    switch (user.Role)
        //    {
        //        case Role.Values.User:
        //            claims.Add(new Claim(ClaimTypes.Role, Role.Name.User));
        //            break;
        //        case Role.Values.Admin:
        //            claims.Add(new Claim(ClaimTypes.Role, Role.Name.Admin));
        //            break;
        //        case Role.Values.Moderator:
        //            claims.Add(new Claim(ClaimTypes.Role, Role.Name.Moderator));
        //            break;
        //        case Role.Values.SuperAdmin:
        //            claims.Add(new Claim(ClaimTypes.Role, Role.Name.SuperAdmin));
        //            break;
        //        default:
        //            break;

        //    }
        //    return claims;
        //}



    }
}
