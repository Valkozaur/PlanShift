namespace PlanShift.Services.Data.BusinessServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessService : IBusinessService
    {
        private readonly IDeletableEntityRepository<Business> businessRepository;

        public BusinessService(IDeletableEntityRepository<Business> businessRepository)
        {
            this.businessRepository = businessRepository;
        }

        public async Task<string> CreateBusinessAsync(string ownerId, string name, string type)
        {
            var business = new Business()
            {
                OwnerId = ownerId,
                Name = name,
                Type = type,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await this.businessRepository.AddAsync(business);
            await this.businessRepository.SaveChangesAsync();

            return business.Id;
        }

        public async Task<string> UpdateBusinessAsync(string businessId, string ownerId = null, string name = null, string type = null)
        {
            var isUpdated = false;

            var business = await this.businessRepository.All().FirstOrDefaultAsync(x => x.Id == businessId);

            if (business != null && (ownerId != null || name != null || type != null))
            {
                business.Name = name ?? business.Name;
                business.OwnerId = ownerId ?? business.OwnerId;
                business.Type = type ?? business.Type;

                isUpdated = true;
            }

            if (isUpdated)
            {
                business.UpdatedAt = DateTime.UtcNow;
            }

            await this.businessRepository.SaveChangesAsync();

            return business?.Id;
        }

        public T GetBusiness<T>(string id) => this.businessRepository.All().Where(b => b.Id == id).To<T>().FirstOrDefault();
    }
}
