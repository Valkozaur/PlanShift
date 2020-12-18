namespace PlanShift.Web.ViewModels.People
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeViewModel : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }

        public string Position { get; set; }

        public string UserFullName { get; set; }
    }
}
