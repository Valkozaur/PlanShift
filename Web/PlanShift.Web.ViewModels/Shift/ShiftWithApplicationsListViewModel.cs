namespace PlanShift.Web.ViewModels.Shift
{
    using System.Collections.Generic;

    public class ShiftWithApplicationsListViewModel
    {
        public IEnumerable<ShiftWithApplicationsViewModel> ShiftsWithApplications { get; set; }

        public string GroupId { get; set; }

        //TODO: Solution for the moment
        public string BusinsesId { get; set; }
    }
}