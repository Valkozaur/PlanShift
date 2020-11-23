namespace PlanShift.Web.ViewModels.ShiftChange
{
    using System.Collections.Generic;

    public class ShiftChangeListViewModel<T>
    {
        public IEnumerable<T> ShiftChanges { get; set; }

        public string BusinessId { get; set; }

        public string GroupId { get; set; }
    }
}