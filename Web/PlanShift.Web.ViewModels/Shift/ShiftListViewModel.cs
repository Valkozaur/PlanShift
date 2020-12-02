namespace PlanShift.Web.ViewModels.Shift
{
    using System.Collections.Generic;

    public class ShiftListViewModel
    {
        public IList<ShiftCalendarViewModel> Shifts { get; set; }

        public int ShiftCount { get; set; }
    }
}
