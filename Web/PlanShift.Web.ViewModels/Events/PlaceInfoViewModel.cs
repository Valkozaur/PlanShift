namespace PlanShift.Web.ViewModels.Events
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class PlaceInfoViewModel : IMapFrom<Place>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}