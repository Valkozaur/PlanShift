namespace PlanShift.Data.Models
{
    using System;

    public class ShiftCalendar
    {
        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}