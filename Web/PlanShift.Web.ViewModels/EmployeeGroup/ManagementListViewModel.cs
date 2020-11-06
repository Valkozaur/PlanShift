namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using System.Collections.Generic;

    public class ManagementListViewModel
    {
        public IEnumerable<ManagementViewModel> Managers { get; set; }
    }
}