namespace PlanShift.Web.ViewModels.Shift
{
    using System;

    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftAllViewModel : IMapFrom<Shift>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Employee { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Shift, ShiftAllViewModel>()
                .ForMember(
                    m => m.Start,
                    s => s.MapFrom(x => x.Start.ToString("g")))
                .ForMember(
                    m => m.End,
                    s => s.MapFrom(x => x.End.ToString("g")))
                .ForMember(
                    m => m.StartDate,
                    s => s.MapFrom(x => x.Start.Date))
                .ForMember(
                    m => m.Status,
                    s => s.MapFrom(x => x.ShiftStatus.ToString()))
                .ForMember(
                    m => m.Employee,
                    s => s.MapFrom(x => x.Employee.Employee.UserName));
        }
    }
}
