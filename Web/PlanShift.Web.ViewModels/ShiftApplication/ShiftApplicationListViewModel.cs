namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using System.Collections.Generic;

    public class ShiftApplicationListViewModel
    {
        public IEnumerable<ShiftApplicationAllViewModel> Applications { get; set; }

        public string ShiftId { get; set; }
    }
}
