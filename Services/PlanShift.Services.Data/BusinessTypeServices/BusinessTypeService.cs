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

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id)
            => await this.businessTypeRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

        public async Task<TViewModel> GetByNameAsync<TViewModel>(string name)
        => await this.businessTypeRepository
            .All()
            .Where(x => x.Name == name)
            .To<TViewModel>()
            .FirstOrDefaultAsync();

        public async Task<int?> GetIdByName(string name)
        {
            var businessType = await this.businessTypeRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            return businessType?.Id;
        }

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>()
        => await this.businessTypeRepository
                .All()
                .To<TViewModel>()
                .ToArrayAsync();
    }
}
