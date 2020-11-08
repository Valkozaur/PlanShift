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

        public async Task<string> AddEmployeeToGroupAsync(string employeeId, string groupId, decimal salary, string position, bool isManagement = false)
        {
            var employeeGroup = new EmployeeGroup()
            {
                EmployeeId = employeeId,
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

        public Task<T> GetEmployeeGroupById<T>(string groupId, string employeeId)
            => this.employeeGroupRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.EmployeeId == employeeId && x.GroupId == groupId)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}
