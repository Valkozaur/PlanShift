namespace PlanShift.Services.Data.BusinessServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessService
    {
        Task<string> CreateBusinessAsync(string ownerId, string name, int typeId);

        Task<string> UpdateBusinessAsync(string businessId, string ownerId, string name = null, int? typeId = null);

        Task<IEnumerable<TViewModel>> GetAllForUserAsync<TViewModel>(string userId);

        Task<T> GetBusinessAsync<T>(string id);
    }
}
