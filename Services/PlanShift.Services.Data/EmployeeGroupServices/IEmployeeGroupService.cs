namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Threading.Tasks;

    using PlanShift.Data.Models;

    public interface IEmployeeGroupService
    {
        Task AddEmployeeToGroupAsync(string groupId, string userId, decimal salary, string position, bool isManagement = false);

        Task<TViewModel> GetEmployeeGroupById<TViewModel>(string groupId, string employeeId);

        EmployeeGroup GetEmployeeGroupByName(string groupName, string employeeId);
    }
}
