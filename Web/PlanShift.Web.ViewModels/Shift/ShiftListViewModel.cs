namespace PlanShift.Web.ViewModels.Shift
{
    using System.Collections.Generic;

    public class ShiftListViewModel
    {
        public IList<ShiftAllViewModel> Shifts { get; set; }

        public string GroupName { get; set; }
    }
}