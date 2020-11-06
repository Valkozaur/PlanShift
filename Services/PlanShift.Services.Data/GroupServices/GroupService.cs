namespace PlanShift.Services.Data.GroupServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupService : IGroupService
    {
        private readonly IDeletableEntityRepository<Group> groupRepository;

        public GroupService(IDeletableEntityRepository<Group> groupRepository)
        {
            this.groupRepository = groupRepository;
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

        public async Task<T> GetGroupAsync<T>(string id) => await this.groupRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
    }
}
