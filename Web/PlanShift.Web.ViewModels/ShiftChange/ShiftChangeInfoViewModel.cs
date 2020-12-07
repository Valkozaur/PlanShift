namespace PlanShift.Web.ViewModels.ShiftChange
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftChangeInfoViewModel : IMapFrom<ShiftChange>
    {
        public string PendingEmployeeId { get; set; }

        public string ShiftId { get; set; }
    }
}
