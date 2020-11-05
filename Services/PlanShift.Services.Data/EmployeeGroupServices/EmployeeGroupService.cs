namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Threading.Tasks;

    using PlanShift.Data.Models;

    public class EmployeeGroupService : IEmployeeGroupService
    {
        public Task AddEmployeeToGroupAsync(string groupId, string userId, decimal salary, string position, bool isManagement = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<TViewModel> GetEmployeeGroupById<TViewModel>(string groupId, string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public EmployeeGroup GetEmployeeGroupByName(string groupName, string employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
