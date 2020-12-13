namespace PlanShift.Web.ViewModels.Shift
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels.Enumerations;

    public class ShiftCalendarViewModel : IMapFrom<Shift>
    {
        public string Id { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [NotMapped]
        public ShiftCalendarType Type { get; set; }
    }
}