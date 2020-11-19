namespace PlanShift.Services.Data.GroupServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Mapping;

    public class GroupService : IGroupService
    {
        private readonly IDeletableEntityRepository<Group> groupRepository;
        private readonly IEmployeeGroupService employeeGroupService;
        private readonly IBusinessService businessService;

        public GroupService(IDeletableEntityRepository<Group> groupRepository, IEmployeeGroupService employeeGroupService, IBusinessService businessService)
        {
            this.groupRepository = groupRepository;
            this.employeeGroupService = employeeGroupService;
            this.businessService = businessService;
        }

        public async Task<string> CreateGroupAsync(string businessId, string name, decimal? standardSalary = null)
        {
            var group = new Group()
            {
                Name = name,
                BusinessId = businessId,
                StandardSalary = standardSalary,
            };

            await this.groupRepository.AddAsync(group);
            await this.groupRepository.SaveChangesAsync();

            var ownerId = await this.businessService.GetOwnerIdAsync(businessId);
            await this.employeeGroupService.AddEmployeeToGroupAsync(ownerId, group.Id, 0, "Owner", true);

            return group.Id;
        }

        public async Task<string> UpdateGroupAsync(
            string groupId,
            string name = null,
            string businessId = null,
            decimal? standardSalary = null)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            if (group != null && (name != null || standardSalary != null))
            {
                group.Name = name ?? group.Name;
                group.BusinessId = businessId;
                group.StandardSalary = standardSalary ?? group.StandardSalary;
            }

            await this.groupRepository.SaveChangesAsync();
            return group?.Id;
        }

        public async Task DeleteGroupAsync(string id)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (group != null)
            {
                this.groupRepository.Delete(group);
                await this.groupRepository.SaveChangesAsync();
            }
        }

        public async Task<T> GetGroupAsync<T>(string id)
            => await this.groupRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllGroupByCurrentUserAndBusinessIdAsync<T>(string businessId, string userId)
            => await this.groupRepository
                .AllAsNoTracking()
                .Where(
                    x => x.BusinessId == businessId && x.Employees.Any(e => e.EmployeeId == userId))
                .To<T>()
                .ToArrayAsync();

        public async Task<string> GetGroupName(string id)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return group.Name;
        }
    }
}
