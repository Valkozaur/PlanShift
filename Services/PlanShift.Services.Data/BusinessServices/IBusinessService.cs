namespace PlanShift.Services.Data.BusinessServices
{
    using System.Threading.Tasks;

    public interface IBusinessService
    {
        Task<string> CreateBusinessAsync(string ownerId, string name, string typeName);

        Task<string> UpdateBusinessAsync(string businessId, string ownerId, string name = null, string typeName = null);

        T GetBusiness<T>(string id);
    }
}
