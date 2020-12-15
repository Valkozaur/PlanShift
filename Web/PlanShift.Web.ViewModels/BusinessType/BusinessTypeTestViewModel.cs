namespace PlanShift.Web.ViewModels.BusinessType
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessTypeTestViewModel : IMapFrom<BusinessType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}