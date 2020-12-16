namespace PlanShift.Web.ViewModels.Shift
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftInfoViewModel : IMapFrom<Shift>, IHaveCustomMappings
    {
        public string OriginalEmployeeId { get; set; }

        public string GroupId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Shift, ShiftInfoViewModel>()
                .ForMember(
                    m => m.OriginalEmployeeId,
                    s => s.MapFrom(x => x.EmployeeId));
        }
    }
}