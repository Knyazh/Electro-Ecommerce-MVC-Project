using E_Commerce_Platform.DataBase.Models;

namespace E_Commerce_Platform.Services.Abstracts;

public interface IUserActivationService
{
    void CreateAndSendActivationtoken(User user) { }
}
