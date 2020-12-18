namespace PlanShift.Web.Hubs
{
    using System.Threading.Tasks;

    public interface IPrivateMessageClient
    {
        Task NewMessage(Message message);
    }
}
