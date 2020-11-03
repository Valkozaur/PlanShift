namespace PlanShift.Services.Data.BusinessServices
{
    using System.Threading.Tasks;

    public interface IBusinessService
    {
        Task<string> CreateBusinessAsync(string ownerId, string name, string type);

        Task<string> UpdateBusinessAsync(string businessId, string ownerId, string name = null, string type = null);

        T GetBusiness<T>(string id);
    }
}
