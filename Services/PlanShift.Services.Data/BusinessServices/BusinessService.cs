namespace PlanShift.Services.Data.BusinessServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessTypeServices;
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

        public async Task<string> CreateBusinessAsync(string ownerId, string name, int typeId)
        {
            var business = new Business()
            {
                OwnerId = ownerId,
                Name = name,
                BusinessTypeId = typeId,
            };

            await this.businessRepository.AddAsync(business);
            await this.businessRepository.SaveChangesAsync();

            return business.Id;
        }

        public async Task<string> UpdateBusinessAsync(string businessId, string ownerId = null, string name = null, int? typeId = null)
        {
            var business = await this.businessRepository.All().FirstOrDefaultAsync(x => x.Id == businessId);

            if (business != null && (ownerId != null || name != null || typeId != null))
            {
                business.Name = name ?? business.Name;
                business.OwnerId = ownerId ?? business.OwnerId;
                business.BusinessTypeId = typeId ?? business.BusinessTypeId;
            }

            await this.businessRepository.SaveChangesAsync();

            return business?.Id;
        }

        public async Task<IEnumerable<T>> GetAllForUserAsync<T>(string userId, int count = 0)
        {
            var query = this.businessRepository
                .AllAsNoTracking()
                .Where(x => x.OwnerId == userId);

            if (count != 0)
            {
                query = query.Take(count);
            }

            return await query.To<T>().ToArrayAsync();
        }

        public async Task<T> GetBusinessAsync<T>(string id)
            => await this.businessRepository
                .All()
                .Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}
