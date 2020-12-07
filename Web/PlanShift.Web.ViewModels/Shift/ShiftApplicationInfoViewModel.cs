namespace PlanShift.Web.ViewModels.Shift
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftApplicationInfoViewModel : IMapFrom<ShiftApplication>
    {
        public string Id { get; set; }

        public string EmployeeUserFullName { get; set; }
    }
}
