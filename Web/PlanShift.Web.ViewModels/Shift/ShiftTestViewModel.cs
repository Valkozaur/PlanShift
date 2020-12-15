namespace PlanShift.Web.ViewModels.Shift
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftTestViewModel : IMapFrom<Shift>
    {
        public string Id { get; set; }
    }
}