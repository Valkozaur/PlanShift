namespace PlanShift.Services.Data.EventsServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        Task CreateAsync(string name, int placeId, DateTime start, DateTime end, string description, string groupId, string creatorId);

        Task<IEnumerable<T>> GetAllEventsPerBusiness<T>(string businessId);

        Task<IEnumerable<T>> GetAllEventsPerBusinessAndUser<T>(string businessId, string userId);

        Task<T> GetEventById<T>(string id);
    }
}