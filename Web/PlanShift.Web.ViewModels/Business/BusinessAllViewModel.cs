namespace PlanShift.Web.ViewModels.Business
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessAllViewModel : IMapFrom<Business>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string BusinessTypeName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Business, BusinessAllViewModel>().ForMember(
                m => m.BusinessTypeName,
                b => b.MapFrom(x => x.BusinessType.Name));
        }
    }
}
