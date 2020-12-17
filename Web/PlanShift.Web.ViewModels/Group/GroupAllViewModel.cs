namespace PlanShift.Web.ViewModels.Group
{
    using System;
    using System.Linq;

    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class GroupAllViewModel : IMapFrom<Group>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int EmployeesCount { get; set; }

        public int ShiftsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Group, GroupAllViewModel>()
                .ForMember(
                    m => m.ShiftsCount,
                    g => g.MapFrom(x => x.Shifts.Count(s => s.End < DateTime.UtcNow)));
        }
    }
}
