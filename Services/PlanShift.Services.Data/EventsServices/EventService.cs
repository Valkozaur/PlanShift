namespace PlanShift.Services.Data.EventsServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;

        public EventService(IDeletableEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task CreateAsync(string name, int placeId, DateTime start, DateTime end, string description, string groupId, string creatorId)
        {
           var @event = new Event()
           {
               Name = name,
               PlaceId = placeId,
               Start = start,
               End = end,
               Description = description,
               CreatorId = creatorId,
           };

           @event.Groups.Add(new GroupEvents() {EventId = @event.Id, @GroupId = groupId});
           await this.eventRepository.AddAsync(@event);
           await this.eventRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllEventsPerBusiness<T>(string businessId)
            => await this.eventRepository
                .AllAsNoTracking()
                .Where(e =>
                     e.Groups.Any(g => g.Group.BusinessId == businessId))
                .To<T>()
                .ToArrayAsync();

        public async Task<IEnumerable<T>> GetAllEventsPerBusinessAndUser<T>(string businessId, string userId)
            => await this.eventRepository
                .AllAsNoTracking()
                .Where(e =>
                    e.Groups.Any(g => g.Group.BusinessId == businessId) &&
                    e.Participants.Any(x => x.UserId == userId))
                .To<T>()
                .ToArrayAsync();

        public async Task<T> GetEventById<T>(string id)
            => await this.eventRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
    }
}