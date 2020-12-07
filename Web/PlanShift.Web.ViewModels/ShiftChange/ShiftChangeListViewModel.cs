namespace PlanShift.Web.ViewModels.ShiftChange
{
    using System.Collections.Generic;

    public class ShiftChangeListViewModel<T>
    {
        public IEnumerable<T> ShiftChanges { get; set; }
    }
}