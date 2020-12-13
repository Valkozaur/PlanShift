namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessTypeService
    {
        Task<int> CreateAsync(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
