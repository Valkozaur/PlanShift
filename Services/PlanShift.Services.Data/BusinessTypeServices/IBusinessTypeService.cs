namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessTypeService
    {
        Task<int> CreateAsync(string name);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<int?> GetIdByName(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
