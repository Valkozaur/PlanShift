namespace PlanShift.Web.ViewModels.Shift
{
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Mapping;

    public class ShiftIdStatusViewModel : IMapFrom<Shift>
    {
        public string GroupId { get; set; }

        public ShiftStatus Status { get; set; }
    }
}
