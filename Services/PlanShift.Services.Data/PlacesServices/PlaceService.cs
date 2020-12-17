namespace PlanShift.Services.Data.PlacesServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Place> placeRepository;

        public PlaceService(IRepository<Place> placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        public async Task<int> CreateAsync(string name, string businessId)
        {
            var place = new Place()
            {
                Name = name,
            };

            place.BusinessPlaces.Add(new BusinessPlaces() {PlaceId = place.Id, BusinessId = businessId} );

            await this.placeRepository.AddAsync(place);
            await this.placeRepository.SaveChangesAsync();

            return place.Id;
        }

        public async Task<IEnumerable<T>> GetAllPlacesByBusinessAsync<T>(string businessId)
            => await this.placeRepository
                .AllAsNoTracking()
                .Where(x => x.BusinessPlaces.Any(x => x.BusinessId == businessId))
                .To<T>()
                .ToArrayAsync();
    }
}