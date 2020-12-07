using System.Threading;

namespace PlanShift.Services.Data.EmployeeGroupServices
{
    using System;
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

        public async Task<string> AddEmployeeToGroupAsync(string userId, string groupId, decimal salary, string position, bool isManagement = false)
        {
            if (await this.IsEmployeeInGroup(userId, groupId))
            {
                throw new ArgumentException("Employee is already in the group!");
            }

            var employeeGroup = new EmployeeGroup()
            {
                UserId = userId,
                GroupId = groupId,
                Salary = salary,
                Position = position,
                IsManagement = isManagement,
            };

            await this.employeeGroupRepository.AddAsync(employeeGroup);
            await this.employeeGroupRepository.SaveChangesAsync();

            return employeeGroup.Id;
        }

        public async Task<IEnumerable<T>> GetAllEmployeesFromGroup<T>(string groupId, bool isManagement = false)
        {
            var queryable = this.employeeGroupRepository.AllAsNoTracking().Where(x => x.GroupId == groupId);

            if (isManagement)
            {
                queryable = queryable.Where(x => x.IsManagement);
            }

            var employees = await queryable.To<T>().ToArrayAsync();

            return employees;
        }

        public Task<T> GetEmployeeGroupById<T>(string employeeId, string userId)
            => this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == employeeId && x.GroupId == userId)
                .To<T>()
                .FirstOrDefaultAsync();

        public Task<bool> IsEmployeeInGroup(string userId, string groupId) => this.employeeGroupRepository.All().AnyAsync(x => x.UserId == userId);

        public async Task<bool> IsEmployeeManagerInGroup(string userId, string groupId)
        {
            return await this.employeeGroupRepository
                .AllAsNoTracking()
                .AnyAsync(x
                    => x.UserId == userId
                    && x.GroupId == groupId
                    && x.IsManagement);
        }

        public async Task<string> GetEmployeeId(string userId, string groupId) =>
            await this.employeeGroupRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId && x.GroupId == groupId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
    }
}
