namespace PlanShift.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class EmployeeGroupEvent : BaseModel<int>
    {
        [Required]
        public string EmployeeGroupId { get; set; }

        public virtual EmployeeGroup EmployeeGroup { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}