﻿namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeGroupService
    {
        Task<string> AddEmployeeToGroupAsync(string userId, string groupId, decimal salary, string position);

        Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId);

        Task<bool> IsEmployeeInGroup(string userId, string groupId);

        Task<bool> IsEmployeeInGroupsWithNames(string userId, string businessId, params string[] groupName);

        Task<string> GetEmployeeId(string userId, string groupId);
    }
}
