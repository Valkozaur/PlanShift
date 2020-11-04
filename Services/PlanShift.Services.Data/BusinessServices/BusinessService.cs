using System.ComponentModel.DataAnnotations;
using PlanShift.Services.Data.BusinessTypeServices;

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
        private readonly IBusinessTypeService businessTypeService;

        public BusinessService(IDeletableEntityRepository<Business> businessRepository, IBusinessTypeService businessTypeService)
        {
            this.businessRepository = businessRepository;
            this.businessTypeService = businessTypeService;
        }

        public async Task<string> CreateBusinessAsync(string ownerId, string name, string typeName)
        {
            var business = new Business()
            {
                OwnerId = ownerId,
                Name = name,
                BusinessTypeId = await this.GetTypeId(typeName),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await this.businessRepository.AddAsync(business);
            await this.businessRepository.SaveChangesAsync();

            return business.Id;
        }

        public async Task<string> UpdateBusinessAsync(string businessId, string ownerId = null, string name = null, string typeName = null)
        {
            var isUpdated = false;

            var business = await this.businessRepository.All().FirstOrDefaultAsync(x => x.Id == businessId);

            if (business != null && (ownerId != null || name != null || typeName != null))
            {
                business.Name = name ?? business.Name;
                business.OwnerId = ownerId ?? business.OwnerId;

                if (typeName != null)
                {
                    business.BusinessTypeId = await this.GetTypeId(typeName);
                }

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

        private async Task<int> GetTypeId(string typeName) 
            => this.businessTypeService.GetByName<BusinessType>(typeName)?.Id ?? await this.businessTypeService.Create(typeName);
    }
}
