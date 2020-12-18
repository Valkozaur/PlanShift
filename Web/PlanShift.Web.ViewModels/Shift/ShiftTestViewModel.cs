namespace PlanShift.Web.ViewModels.Shift
{
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Mapping;

    public class ShiftTestViewModel : IMapFrom<Shift>
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public string GroupId { get; set; }

        public ShiftStatus ShiftStatus { get; set; }
    }
}
