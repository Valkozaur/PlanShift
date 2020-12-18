namespace PlanShift.Web.ViewModels.Shift
{
    using System;
    using System.Collections.Generic;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftWithApplicationsViewModel : IMapFrom<Shift>
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Position { get; set; }

        public decimal BonusPayment { get; set; }

        public string Description { get; set; }

        public IEnumerable<ShiftApplicationInfoViewModel> ShiftApplications { get; set; }
    }
}
