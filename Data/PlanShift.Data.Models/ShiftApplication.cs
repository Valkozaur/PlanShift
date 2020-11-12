using PlanShift.Data.Models.Enumerations;

namespace PlanShift.Data.Models
{
    using System;

    using PlanShift.Data.Common.Models;

    public class ShiftApplication : BaseModel<string>
    {
        public ShiftApplication()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ShiftId { get; set; }

        public Shift Shift { get; set; }

        public string EmployeeId { get; set; }

        public EmployeeGroup Employee { get; set; }

        public ShiftApplicationStatus Status { get; set; }
    }
}
