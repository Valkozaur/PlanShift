namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeGroupService
    {
        Task<string> AddEmployeeToGroupAsync(string userId, string groupId, decimal salary, string position, bool isManagement = false);

        Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId);

        Task<T> GetEmployeeGroupById<T>(string userId, string groupId);

        Task<bool> IsEmployeeInGroup(string userId, string groupId);

        Task<bool> IsEmployeeInGroupWithName(string userId, string businessId, string groupName);

        Task<bool> IsEmployeeInGroupsWithNames(string userId, string businessId, params string[] groupName);

        Task<string> GetEmployeeId(string userId, string groupId);
    }
}
