namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupIsManagementInfo : IMapFrom<EmployeeGroup>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public bool IsManagement { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeeGroup, EmployeeGroupIsManagementInfo>()
                .ForMember(
                    m => m.IsManagement,
                    eg => eg.MapFrom(x => x.IsManagement));
        }
    }
}
