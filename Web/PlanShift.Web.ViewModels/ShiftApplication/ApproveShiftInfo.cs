namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ApproveShiftInfo : IMapFrom<ShiftApplication>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public string ShiftId { get; set; }

        public string GroupId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShiftApplication, ApproveShiftInfo>()
                .ForMember(
                    m => m.GroupId,
                    sa
                        => sa.MapFrom(x => x.Shift.GroupId));
        }
    }
}
