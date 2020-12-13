namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupIdViewModel : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }
    }
}
