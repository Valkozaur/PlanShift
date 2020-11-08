namespace PlanShift.Web.ViewModels.Shift
{
    using System;

    public class CreateShiftInputModel
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string GroupId { get; set; }

        public decimal? BonusPayment { get; set; }
    }
}