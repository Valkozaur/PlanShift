namespace PlanShift.Web.ViewModels.Business
{
    using PlanShift.Services.Mapping;
    using PlanShift.Data.Models;

    public class BusinessTestViewModel : IMapFrom<Business>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}