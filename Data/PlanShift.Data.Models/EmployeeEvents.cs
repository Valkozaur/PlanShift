namespace PlanShift.Data.Models
{
    using PlanShift.Data.Common.Models;

    public class EmployeeEvents : BaseModel<int>
    {
        public string EmployeeId { get; set; }

        public virtual EmployeeGroup EmployeeGroup { get; set; }

        public string EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}