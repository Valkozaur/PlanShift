namespace PlanShift.Data.Models
{
    using PlanShift.Data.Common.Models;

    public class ShiftApplication : BaseModel<string>
    {
        public string ShiftId { get; set; }

        public Shift Shift { get; set; }

        public string EmployeeId { get; set; }

        public EmployeeGroup Employee { get; set; }
    }
}
