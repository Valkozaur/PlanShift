namespace SignalRChat.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using PlanShift.Web.Hubs;

    [Authorize]
    public class ChatHub : Hub<IPrivateMessageClient>
    {
        public Task Send(string message)
        {
            return this.Clients.All.NewMessage(new Message() {User = this.Context.User.Identity.Name, Text = message});
        }
    }
}