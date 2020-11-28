namespace PlanShift.Web.ViewModels.Group
{
    using System.Collections.Generic;

    using PlanShift.Web.ViewModels.Shift;

    public class GroupListViewModel<T>
    {
        public IEnumerable<T> Groups { get; set; }

        public string BusinessId { get; set; }

        public string ActiveTabGroupId { get; set; }
    }
}
