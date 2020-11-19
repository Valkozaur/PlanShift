namespace PlanShift.Web.ViewModels.Shift
{
    using System.Collections.Generic;

    using AutoMapper;
    using PlanShift.Services.Mapping;

    public class ShiftWithApplicationsViewModel : IMapFrom<Data.Models.Shift>, IHaveCustomMappings
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public IEnumerable<ShiftApplicationInfoViewModel> Applications { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Shift, ShiftWithApplicationsViewModel>()
                .ForMember(
                    m => m.StartDate,
                    s => s.MapFrom(x => x.Start))
                .ForMember(
                    m => m.EndDate,
                    s => s.MapFrom(x => x.End));
        }
    }
}