namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupApplicationViewModel : IMapFrom<EmployeeGroup>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeeGroup, EmployeeGroupApplicationViewModel>()
                .ForMember(
                    m => m.Name,
                    eg => eg.MapFrom(x => x.Employee.UserName));
        }
    }
}
