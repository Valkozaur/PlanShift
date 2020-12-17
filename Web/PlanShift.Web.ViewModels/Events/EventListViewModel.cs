namespace PlanShift.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class EventListViewModel<T>
    {
        public IEnumerable<T> Events { get; set; }
    }
}