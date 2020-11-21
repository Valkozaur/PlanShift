namespace PlanShift.Web.ViewModels.Shift
{
    using System;

    using AutoMapper;
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftShiftApplicationViewModel : IMapFrom<Shift>
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public decimal BonusPayment { get; set; }
    }
}
