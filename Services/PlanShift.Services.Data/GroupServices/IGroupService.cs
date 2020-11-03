namespace PlanShift.Services.Data.GroupServices
{
    using System.Threading.Tasks;

    using PlanShift.Data.Models;

    public interface IGroupService
    {
        Task<string> CreateGroupAsync(string name, string businessId, decimal? averageSalary = null, decimal? standardSalary = null);

        Task<string> UpdateGroupAsync(
            string groupId,
            string name = null,
            string businessId = null,
            decimal? averageSalary = null,
            decimal? standardSalary = null);

        Task DeleteGroupAsync(string id);

        Task<T> GetGroupAsync<T>(string id);
    }
}
