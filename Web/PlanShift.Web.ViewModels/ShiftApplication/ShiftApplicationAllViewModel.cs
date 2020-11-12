namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftApplicationAllViewModel : IMapFrom<ShiftApplication>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string EmployeeName { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShiftApplication, ShiftApplicationAllViewModel>()
                .ForMember(
                    m => m.EmployeeName,
                    sa =>
                        sa.MapFrom(x => x.Employee.Employee.UserName));
        }
    }
}
