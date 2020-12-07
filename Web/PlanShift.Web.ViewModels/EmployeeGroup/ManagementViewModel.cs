namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ManagementViewModel : IMapFrom<EmployeeGroup>
    {
        public string Id { get; set; }

        public string UserFullName { get; set; }

        public int CreatedShiftsCount { get; set; }

        public int ManagedShiftsCount { get; set; }
    }
}
