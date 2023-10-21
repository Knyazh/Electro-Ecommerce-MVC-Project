using E_Commerce_Platform.DataBase.Models;

namespace E_Commerce_Platform.Services.Abstracts;

public interface IAlertMessageService
{
    void AddConnectionId(User user, string connectionId);
    void RemoveConnectionId(User user, string connectionId);
    List<string> GetConnectionIds(User user);
}
