namespace PlanShift.Web.ViewModels.Shift
{
    using AutoMapper;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftShiftApplicationViewModel : IMapFrom<Shift>, IHaveCustomMappings
    {
        public string Start { get; set; }

        public string End { get; set; }

        public string Description { get; set; }

        public decimal BonusPayment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Shift, ShiftShiftApplicationViewModel>()
            .ForMember(
                    m => m.Start,
                    s => s.MapFrom(x => x.Start.ToString("g")))
                .ForMember(
                    m => m.End,
                    s => s.MapFrom(x => x.End.ToString("g")));
        }
    }
}
