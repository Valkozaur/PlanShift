namespace PlanShift.Web.Hubs
{
    using System.Threading.Tasks;

    public interface ITestChatHub
    {
        Task UserLoggedOn(object args);

        Task UserLoggedOff(object args);

        Task UserTyping(object args);

        Task MessageReceived(Message message);

        Task JoinGroup(string key);
    }
}