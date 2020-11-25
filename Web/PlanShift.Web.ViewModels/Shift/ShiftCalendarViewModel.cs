namespace PlanShift.Web.ViewModels.Shift
{
    using System;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftCalendarViewModel : IMapFrom<Shift>
    {
        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}