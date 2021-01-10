namespace PlanShift.Web.ViewModels.Business
{
    public class BusinessIndexViewModel
    {
        public int ShiftChangesCount { get; set; }

        public int ShiftApplicationsCount { get; set; }

        public bool IsScheduleManagerOrAdmin { get; set; }

        public string BusinessId { get; set; }
    }
}
