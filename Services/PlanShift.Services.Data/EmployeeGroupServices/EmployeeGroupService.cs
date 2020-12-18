using PlanShift.Common;

namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupService : IEmployeeGroupService
    {
        private readonly IDeletableEntityRepository<EmployeeGroup> employeeGroupRepository;

        public EmployeeGroupService(IDeletableEntityRepository<EmployeeGroup> employeeGroupRepository)
        {
            this.employeeGroupRepository = employeeGroupRepository;
        }

        public async Task<string> AddEmployeeToGroupAsync(string userId, string groupId, decimal salary, string position)
        {
            var employeeGroup = new EmployeeGroup()
            {
                UserId = userId,
                GroupId = groupId,
                Salary = salary,
                Position = position,
            };

            await this.employeeGroupRepository.AddAsync(employeeGroup);
            await this.employeeGroupRepository.SaveChangesAsync();

            return employeeGroup.Id;
        }

        public async Task RemoveFromGroupEmployeeAsync(string id)
        {
            var employee = await this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(eg => eg.Id == id)
                .FirstOrDefaultAsync();

            if (employee != null)
            {
                this.employeeGroupRepository.Delete(employee);
            }

            await this.employeeGroupRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId)
            => await this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(x => x.GroupId == groupId)
                .To<T>()
                .ToArrayAsync();

        public Task<bool> IsEmployeeInGroup(string userId, string groupId)
            => this.employeeGroupRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.GroupId == groupId);

        public async Task<bool> IsEmployeeInGroupsWithNames(string userId, string businessId, params string[] groupName)
            => await this.employeeGroupRepository
                .AllAsNoTracking()
                .AnyAsync(eg
                    => eg.UserId == userId &&
                       eg.Group.BusinessId == businessId &&
                       groupName.Contains(eg.Group.Name));

        public async Task<string> GetFirstEmployeeIdFromAdministrationGroups(string userId, string businessId)
            => await this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(eg
                    => eg.UserId == userId &&
                       eg.Group.BusinessId == businessId &&
                       eg.Group.Name == GlobalConstants.AdminsGroupName ||
                       eg.Group.Name == GlobalConstants.HrGroupName ||
                       eg.Group.Name == GlobalConstants.ScheduleManagersGroupName)
                .Select(eg => eg.Id)
                .FirstOrDefaultAsync();

        public async Task<string> GetEmployeeId(string userId, string groupId)
            => await this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId && x.GroupId == groupId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
    }
}
