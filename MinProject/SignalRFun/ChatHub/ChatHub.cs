using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MinProject.Data;
using System;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    //private readonly ApplicationDbContext _context;

    //public ChatHub(ApplicationDbContext context)
    //{
    //    _context = context;
    //}

    //public override Task OnConnectedAsync()
    //{
    //    var userId = Context.GetHttpContext().Session.GetString("UserId");
    //    if (userId != null)
    //    {
    //        Groups.AddToGroupAsync(Context.ConnectionId, userId);
    //    }
    //    return base.OnConnectedAsync();
    //}

    //public async Task SendMessage(string receiverId, string message)
    //{
    //    var senderId = Context.GetHttpContext().Session.GetString("UserId");

    //    if (senderId == null || receiverId == null || message == null)
    //    {
    //        return;
    //    }

    //    var newMessage = new MinProject.Views.Message.MessageModel
    //    {
    //        SenderId = Guid.Parse(senderId),
    //        ReceiverId = Guid.Parse(receiverId),
    //        Content = message,
    //        Timestamp = DateTime.UtcNow
    //    };

    //    _context.Messages.Add(newMessage);
    //    await _context.SaveChangesAsync();

    //    await Clients.Group(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message);
    //    await Clients.Group(senderId).SendAsync("ReceiveMessage", senderId, receiverId, message);
    //}




    private readonly ApplicationDbContext _context;
    private readonly PresenceTracker _presenceTracker;

    public ChatHub(ApplicationDbContext context, PresenceTracker presenceTracker)
    {
        _context = context;
        _presenceTracker = presenceTracker;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext().Session.GetString("UserId");
        if (userId != null)
        {
            await _presenceTracker.ConnectionOpened(userId);
            Groups.AddToGroupAsync(Context.ConnectionId, userId);

            var currentUsers = await _presenceTracker.GetOnlineUsers();
            var currentUserNames = await _context.Users
                .Where(u => currentUsers.Contains(u.Id.ToString()))
                .Select(u => u.Name)
                .ToListAsync();

            await Clients.Caller.SendAsync("UpdateUserList", currentUserNames);
            await Clients.All.SendAsync("UserJoined", userId, Context.User.Identity.Name);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.GetHttpContext().Session.GetString("UserId");
        if (userId != null)
        {
            var result = await _presenceTracker.ConnectionClosed(userId);
            if (result.UserLeft)
            {
                await Clients.All.SendAsync("UserLeft", userId, Context.User.Identity.Name);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string receiverId, string message)
    {
        var senderId = Context.GetHttpContext().Session.GetString("UserId");

        if (senderId == null || receiverId == null || message == null)
        {
            return;
        }

        var newMessage = new MinProject.Views.Message.MessageModel
        {
            SenderId = Guid.Parse(senderId),
            ReceiverId = Guid.Parse(receiverId),
            Content = message,
            Timestamp = DateTime.UtcNow
        };

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        await Clients.Group(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message);
        await Clients.Group(senderId).SendAsync("ReceiveMessage", senderId, receiverId, message);
    }

    //public override async Task OnConnectedAsync()
    //{
    //    var userId = Context.GetHttpContext().Session.GetString("UserId");
    //    if (userId != null)
    //    {
    //        await _presenceTracker.ConnectionOpened(userId);
    //        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    //        var currentUsers = await _presenceTracker.GetOnlineUsers();
    //        await Clients.Caller.SendAsync("UpdateUserList", currentUsers);
    //        await Clients.All.SendAsync("UserJoined", userId);
    //    }
    //    await base.OnConnectedAsync();
    //}

    //public override async Task OnDisconnectedAsync(Exception exception)
    //{
    //    var userId = Context.GetHttpContext().Session.GetString("UserId");
    //    if (userId != null)
    //    {
    //        var result = await _presenceTracker.ConnectionClosed(userId);
    //        if (result.UserLeft)
    //        {
    //            await Clients.All.SendAsync("UserLeft", userId);
    //        }
    //    }
    //    await base.OnDisconnectedAsync(exception);
    //}

    //public async Task SendMessage(string receiverId, string message)
    //{
    //    var senderId = Context.GetHttpContext().Session.GetString("UserId");

    //    if (senderId == null || receiverId == null || message == null)
    //    {
    //        return;
    //    }

    //    var newMessage = new MinProject.Views.Message.MessageModel
    //    {
    //        SenderId = Guid.Parse(senderId),
    //        ReceiverId = Guid.Parse(receiverId),
    //        Content = message,
    //        Timestamp = DateTime.UtcNow
    //    };

    //    _context.Messages.Add(newMessage);
    //    await _context.SaveChangesAsync();

    //    await Clients.Group(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message);
    //    await Clients.Group(senderId).SendAsync("ReceiveMessage", senderId, receiverId, message);
    //}


}


public class PresenceTracker
{
    private static readonly Dictionary<string, int> onlineUsers = new Dictionary<string, int>();

    public Task<ConnectionOpenedResult> ConnectionOpened(string userId)
    {
        var joined = false;
        lock (onlineUsers)
        {
            if (onlineUsers.ContainsKey(userId))
            {
                onlineUsers[userId] += 1;
            }
            else
            {
                onlineUsers.Add(userId, 1);
                joined = true;
            }
        }
        return Task.FromResult(new ConnectionOpenedResult { UserJoined = joined });
    }

    public Task<ConnectionClosedResult> ConnectionClosed(string userId)
    {
        var left = false;
        lock (onlineUsers)
        {
            if (onlineUsers.ContainsKey(userId))
            {
                onlineUsers[userId] -= 1;
                if (onlineUsers[userId] <= 0)
                {
                    onlineUsers.Remove(userId);
                    left = true;
                }
            }
        }

        return Task.FromResult(new ConnectionClosedResult { UserLeft = left });
    }

    public Task<string[]> GetOnlineUsers()
    {
        lock (onlineUsers)
        {
            return Task.FromResult(onlineUsers.Keys.ToArray());
        }
    }
}

public class ConnectionOpenedResult
{
    public bool UserJoined { get; set; }
}

public class ConnectionClosedResult
{
    public bool UserLeft { get; set; }
}

