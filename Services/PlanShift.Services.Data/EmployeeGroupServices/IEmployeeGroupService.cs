namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeGroupService
    {
        Task<string> AddEmployeeToGroupAsync(string employeeId, string groupId, decimal salary, string position, bool isManagement = false);

        Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId, bool isManagement = false);

        Task<T> GetEmployeeGroupById<T>(string groupId, string employeeId);

        Task<bool> IsEmployeeManagerInGroup(string employeeId, string groupId);
    }
}
