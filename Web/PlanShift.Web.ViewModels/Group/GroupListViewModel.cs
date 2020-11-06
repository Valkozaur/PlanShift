namespace PlanShift.Web.ViewModels.Group
{
    using System.Collections.Generic;

    public class GroupListViewModel
    {
        public IEnumerable<GroupAllViewModel> Groups { get; set; }

        public string BusinessId { get; set; }
    }
}