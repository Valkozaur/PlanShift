namespace PlanShift.Services.Data.BusinessTypeServices
{
    using System.Threading.Tasks;

    public interface IBusinessTypeService
    {
        Task<int> Create(string name);

        Task<TViewModel> GetById<TViewModel>(int id);

        Task<TViewModel> GetByName<TViewModel>(string name);
    }
}