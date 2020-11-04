namespace PlanShift.Services.Data.BusinessTypeServices
{
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

        public async Task<int> Create(string name)
        {
            var businessType = new BusinessType
            {
                Name = name,
            };

            await this.businessTypeRepository.AddAsync(businessType);
            await this.businessTypeRepository.SaveChangesAsync();

            return businessType.Id;
        }

        public async Task<TViewModel> GetById<TViewModel>(int id)
            => await this.businessTypeRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();


        public async Task<TViewModel> GetByName<TViewModel>(string name)
        => await this.businessTypeRepository
            .All()
            .Where(x => x.Name == name)
            .To<TViewModel>()
            .FirstOrDefaultAsync();
    }
}