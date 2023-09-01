using Microsoft.AspNetCore.SignalR;

namespace Aplication.Utils.Signalr;

public class SignalrHub : Hub
{
    private static List<UserIdentitySignalrHub> UsersLogado { get; set; } = new();
    
    public void NewMessage(object message, int idUsuario)
    {
        var userEnvio = UsersLogado.FirstOrDefault(x => x.IdUsuario == idUsuario);
        
        if (userEnvio != null && userEnvio.IdUsuarioSignalr != null)
        {
            Clients.Client(userEnvio.IdUsuarioSignalr).SendAsync("newMessage", message);
        }
    }

    public void ConnectOnline(int userId)
    {
        if (UsersLogado.All(x => x.IdUsuario != userId))
        {
            UsersLogado.Add(new UserIdentitySignalrHub()
            {
                IdUsuario = userId,
                IdUsuarioSignalr = Context.ConnectionId
            });
        }
        else
        {
            var updateUser = UsersLogado.FirstOrDefault(x => x.IdUsuario == userId);

            if (updateUser != null) 
                updateUser.IdUsuarioSignalr = Context.ConnectionId;
        }
        
        var user = UsersLogado.FirstOrDefault(x => x.IdUsuario == userId);

        Clients.All.SendAsync("connectOnline", UsersLogado);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = UsersLogado.FirstOrDefault(x => x.IdUsuarioSignalr == Context.ConnectionId);
        UsersLogado.RemoveAt(UsersLogado.FindIndex(x => x.IdUsuarioSignalr == Context.ConnectionId));
        
        await Clients.All.SendAsync("onDisconnectedAsync", UsersLogado);
        await base.OnDisconnectedAsync(exception);
    }
    
    public List<UserIdentitySignalrHub> GetConnectedUsers()
    {
        return UsersLogado;
    }
}

public class UserIdentitySignalrHub
{
    public int IdUsuario { get; set; }
    public string? IdUsuarioSignalr { get; set; }
}