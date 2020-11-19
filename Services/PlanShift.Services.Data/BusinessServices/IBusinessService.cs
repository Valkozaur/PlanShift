namespace PlanShift.Services.Data.BusinessServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessService
    {
        Task<string> CreateBusinessAsync(string ownerId, string name, int typeId);

        Task<string> UpdateBusinessAsync(string businessId, string ownerId, string name = null, int? typeId = null);

        Task<IEnumerable<T>> GetAllForUserAsync<T>(string userId, int count = 0);

        Task<T> GetBusinessAsync<T>(string id);

        Task<string> GetOwnerIdAsync(string id);
    }
}
