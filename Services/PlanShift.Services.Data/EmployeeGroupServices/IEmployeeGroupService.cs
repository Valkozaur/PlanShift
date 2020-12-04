namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeGroupService
    {
        Task<string> AddEmployeeToGroupAsync(string userId, string groupId, decimal salary, string position, bool isManagement = false);

        Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId, bool isManagement = false);

        Task<T> GetEmployeeGroupById<T>(string groupId, string userId);

        Task<bool> IsEmployeeInGroup(string userId, string groupId);

        Task<bool> IsEmployeeManagerInGroup(string userId, string groupId);

        Task<string> GetEmployeeId(string userId, string groupId);
    }
}
