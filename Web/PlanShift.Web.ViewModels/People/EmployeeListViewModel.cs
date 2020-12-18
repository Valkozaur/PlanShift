namespace PlanShift.Web.ViewModels.People
{
    using System.Collections.Generic;

    public class EmployeeListViewModel<T>
    {
        public IEnumerable<T> Employees { get; set; }
    }
}
