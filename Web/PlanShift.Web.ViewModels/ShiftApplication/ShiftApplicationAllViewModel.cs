namespace PlanShift.Web.ViewModels.ShiftApplication
{
    using System.Collections.Generic;

    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels.EmployeeGroup;

    public class ShiftApplicationAllViewModel : IMapFrom<ShiftApplication>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string GroupName { get; set; }

        public string Date { get; set; }

        public IEnumerable<EmployeeGroupApplicationViewModel> AppliedEmployees { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShiftApplication, ShiftApplicationAllViewModel>()
                .ForMember(
                    m => m.GroupName,
                    sa
                        => sa.MapFrom(x => x.Shift.Group.Name))
                .ForMember(
                    m => m.Date,
                    sa
                        => sa.MapFrom(x => x.Shift.Start.ToString("D")));
        }
    }
}
