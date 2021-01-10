namespace PlanShift.Services.Data.EventServices
{
    using System;
    using System.Collections.Generic;

    public interface IEventService
    {
        public int Create(string name, DateTime start, bool isInvitationOnly, string creatorId, DateTime? endDate = null);

        public T GetById<T>(int id);

        public IEnumerable<T> GetAllPerBusiness<T>(string businessId);
    }
}