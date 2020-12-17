namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeIdGroupIdModel : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }

        public string GroupId { get; set; }
    }
}