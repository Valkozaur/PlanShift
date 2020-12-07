namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using System;
    using System.Linq;

    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EmployeeGroupInfoViewModel : IMapFrom<EmployeeGroup>, IHaveCustomMappings
    {
        public string UserFullName { get; set; }

        public string Position { get; set; }

        public int ActiveShifts { get; set; }

        public int ShiftsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeeGroup, EmployeeGroupInfoViewModel>()
                .ForMember(
                    m => m.ActiveShifts,
                    eg => eg.MapFrom(x => x.Shifts.Count(s => s.Start > DateTime.UtcNow)));
        }
    }
}
