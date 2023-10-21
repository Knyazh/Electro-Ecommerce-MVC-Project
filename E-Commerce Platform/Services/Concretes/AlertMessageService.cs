using E_Commerce_Platform.DataBase.Models;
using E_Commerce_Platform.Services.Abstracts;

namespace E_Commerce_Platform.Services.Concretes;

public class AlertMessageService : IAlertMessageService
{
    private readonly Dictionary<int, List<string>> _userConnectionIds;

    public AlertMessageService()
    {
        _userConnectionIds = new Dictionary<int, List<string>>();
    }

    public void AddConnectionId(User user, string connectionId)
    {
       lock (_userConnectionIds)
        {
            if (_userConnectionIds.ContainsKey(user.Id))
            {
                List<string> connectionIds = _userConnectionIds[user.Id];
                connectionIds.Add(connectionId);
            }

            else
            {
                _userConnectionIds.Add(user.Id, new List<string> { connectionId });
            }
        }
    }

    public List<string> GetConnectionIds(User user)
    {
        lock (_userConnectionIds)
        {
            if (_userConnectionIds.ContainsKey(user.Id))
            {
                return _userConnectionIds[user.Id];
            }

            return new List<string>();
        }
    }

    public void RemoveConnectionId(User user, string connectionId)
    {
        lock (_userConnectionIds)
        {
            List<string> connectionIds = _userConnectionIds[user.Id];
            connectionIds.Remove(connectionId);

            if (!connectionIds.Any())
            {
                _userConnectionIds.Remove(user.Id);
            }
        }
    }
}
