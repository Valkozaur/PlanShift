namespace PlanShift.Web.ViewModels.Shift
{
    using System.Collections.Generic;

    public class ShiftWithApplicationsListViewModel
    {
        public IEnumerable<ShiftWithApplicationsViewModel> ShiftsWithApplications { get; set; }
    }
}