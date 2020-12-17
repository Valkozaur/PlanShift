namespace PlanShift.Web.ViewModels.Events
{
    using System;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EventBasicInfoViewModel : IMapFrom<Event>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}