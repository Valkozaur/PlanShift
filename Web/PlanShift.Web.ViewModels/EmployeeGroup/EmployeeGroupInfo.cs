namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupInfo : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }

        public bool IsManagement { get; set; }
    }
}
