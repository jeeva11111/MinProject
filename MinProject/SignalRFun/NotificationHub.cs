using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace MinProject.SignalRFun
{
    public class NotificationHub { }
}
    
    /*: Hub*/
    
    //    private readonly ConnectionMapping _mapping;
    //    private readonly IHttpContextAccessor _contextAccessor;
    //    public NotificationHub(ConnectionMapping mapping, IHttpContextAccessor contextAccessor)
    //    {
    //        _mapping = mapping;
    //        _contextAccessor = contextAccessor;
    //    }

    //    public async Task SendNotification(string message)
    //    {
    //        await Clients.All.SendAsync("ReceiveNotification", message);
    //    }

    //    public override Task OnConnectedAsync()
    //    {

    //        var userId = _contextAccessor.HttpContext.Session.GetString("UserId");
    //        if (string.IsNullOrEmpty(userId))
    //        {
    //            _mapping.Add(userId, Context.ConnectionId);
    //        }

    //        return base.OnConnectedAsync();
    //    }


    //    public override Task OnDisconnectedAsync(Exception? exception)
    //    {

    //        _mapping.Remove(Context.ConnectionId);
    //        return base.OnDisconnectedAsync(exception);
    //    }

    //}

    //public class ConnectionMapping
    //{
    //    private readonly ConcurrentDictionary<string, string> _connection = new ConcurrentDictionary<string, string>();

    //    public void Add(string UserId, string ConnectionId)
    //    {
    //        _connection[UserId] = ConnectionId;
    //    }

    //    public void Remove(string ConnectionId)
    //    {
    //        var userId = _connection.FirstOrDefault(x => x.Value == ConnectionId).Key;

    //        if (userId != null)
    //        {
    //            _connection.TryGetValue(userId, out _);
    //        }
    //    }


    //    public string GetConnectionId(string userId)
    //    {
    //        _connection.TryGetValue(userId, out var connectionId);

    //        return connectionId;
    //    }
    //}

