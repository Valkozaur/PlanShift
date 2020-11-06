namespace PlanShift.Services.Data.UserServices
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> CreateUser(string name, string password, string email);

        Task<T> GetUser<T>(string id);

        Task UpdateUser(string id, string name = null, string password = null, string email = null);

        Task DeleteUser(string id);
    }
}
