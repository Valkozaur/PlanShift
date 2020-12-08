namespace PlanShift.Web.ViewModels.ShiftChange
{
    using System;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftChangeUserViewModel : IMapFrom<ShiftChange>
    {
        public string Id { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public string ShiftPosition { get; set; }

        public string ShiftDescription { get; set; }

        public string ShiftGroupName { get; set; }

        public string PendingEmployeeUserFullName { get; set; }
    }
}