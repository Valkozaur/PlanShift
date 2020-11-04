namespace PlanShift.Web.ViewModels.Business
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessTypeDto : IMapFrom<BusinessType>
    {
        public int Id { get; set; }
    }
}