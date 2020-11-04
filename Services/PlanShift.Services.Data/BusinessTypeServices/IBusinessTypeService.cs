namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessTypeService
    {
        Task<int> CreateAsync(string name);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id);

        Task<TViewModel> GetByNameAsync<TViewModel>(string name);

        Task<int> GetIdByName(string name);

        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>();
    }
}
