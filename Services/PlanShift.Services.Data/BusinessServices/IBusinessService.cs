namespace PlanShift.Services.Data.BusinessServices
{
    using System.Threading.Tasks;

    public interface IBusinessService
    {
        Task<string> CreateBusinessAsync(string ownerId, string name, int typeId);

        Task<string> UpdateBusinessAsync(string businessId, string ownerId, string name = null, int? typeId = null);

        T GetBusiness<T>(string id);
    }
}
