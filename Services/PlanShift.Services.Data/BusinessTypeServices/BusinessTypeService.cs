namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessTypeService : IBusinessTypeService
    {
        private readonly IRepository<BusinessType> businessTypeRepository;

        public BusinessTypeService(IRepository<BusinessType> businessTypeRepository)
        {
            this.businessTypeRepository = businessTypeRepository;
        }

        public async Task<int> CreateAsync(string name)
        {
            var businessType = new BusinessType
            {
                Name = name,
            };

            await this.businessTypeRepository.AddAsync(businessType);
            await this.businessTypeRepository.SaveChangesAsync();

            return businessType.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        => await this.businessTypeRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArrayAsync();
    }
}
