namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<T> GetByIdAsync<T>(int id)
            => await this.businessTypeRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<T> GetByNameAsync<T>(string name)
        => await this.businessTypeRepository
            .All()
            .Where(x => x.Name == name)
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task<int?> GetIdByName(string name)
        {
            var businessType = await this.businessTypeRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            return businessType?.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        => await this.businessTypeRepository
                .All()
                .To<T>()
                .ToArrayAsync();
    }
}
