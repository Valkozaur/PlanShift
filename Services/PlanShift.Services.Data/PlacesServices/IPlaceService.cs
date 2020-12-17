namespace PlanShift.Services.Data.PlacesServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceService
    {
        Task<int> CreateAsync(string name, string businessId);

        Task<IEnumerable<T>> GetAllPlacesByBusinessAsync<T>(string businessId);
    }
}