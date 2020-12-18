namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Mapping;

    public class ShiftApplicationTestViewModel : IMapFrom<ShiftApplication>
    {
        public string Id { get; set; }

        public ShiftApplicationStatus Status { get; set; }
    }
}
