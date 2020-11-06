namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ManagementViewModel : IMapFrom<EmployeeGroup>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public int CreatedShiftsCount { get; set; }

        public int ManagedShiftsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeeGroup, ManagementViewModel>().ForMember(
                m => m.Username,
                eg => eg.MapFrom(x => x.Employee.UserName));
        }
    }
}
