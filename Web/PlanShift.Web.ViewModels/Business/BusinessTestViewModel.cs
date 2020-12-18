namespace PlanShift.Web.ViewModels.Business
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessTestViewModel : IMapFrom<Business>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
