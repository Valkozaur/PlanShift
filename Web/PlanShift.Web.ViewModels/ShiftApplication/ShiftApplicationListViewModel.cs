namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using System.Collections.Generic;

    using PlanShift.Web.ViewModels.Shift;

    public class ShiftApplicationListViewModel
    {
        public IEnumerable<ShiftApplicationAllViewModel> Applications { get; set; }

        public ShiftShiftApplicationViewModel Shift { get; set; }
    }
}
