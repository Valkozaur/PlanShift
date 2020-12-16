namespace PlanShift.Web.ViewModels.Business
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessNameViewModel : IMapFrom<Business>
    {
        public string Name { get; set; }
    }
}