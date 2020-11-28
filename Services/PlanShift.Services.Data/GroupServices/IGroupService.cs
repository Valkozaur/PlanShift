namespace PlanShift.Services.Data.GroupServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Services.Data.Enumerations;

    public interface IGroupService
    {
        Task<string> CreateGroupAsync(string businessId, string name, decimal? standardSalary = null);

        Task<string> UpdateGroupAsync(
            string groupId,
            string name = null,
            string businessId = null,
            decimal? standardSalary = null);

        Task DeleteGroupAsync(string id);

        Task<T> GetGroupAsync<T>(string id);

        Task<IEnumerable<T>> GetAllGroupByCurrentUserAndBusinessIdAsync<T>(string businessId, string userId,bool isManager = false, PendingActionsType pendingAction = PendingActionsType.Unknown);

        Task<string> GetGroupName(string id);

        Task<string> GetGroupsBusinessId(string groupId);
    }
}
