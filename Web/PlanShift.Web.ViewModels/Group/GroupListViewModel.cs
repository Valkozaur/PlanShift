namespace PlanShift.Web.ViewModels.Group
{
    using System.Collections.Generic;

    using PlanShift.Web.ViewModels.Shift;

    public class GroupListViewModel<T>
    {
        public IEnumerable<T> Groups { get; set; }

        public IEnumerable<T> SpecialGroups { get; set; }

        public string ActiveTabGroupId { get; set; }

        public bool IsInHrOrAdminRoleGroup { get; set; }
    }
}
