// ReSharper disable VirtualMemberCallInConstructor

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class Group : BaseDeletableModel<string>
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Employees = new HashSet<EmployeeGroup>();
            this.Shifts = new HashSet<Shift>();
            this.Events = new HashSet<GroupEvents>();
        }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        public decimal? StandardSalary { get; set; }

        [Required]
        public string BusinessId { get; set; }

        public virtual Business Business { get; set; }

        public virtual ICollection<EmployeeGroup> Employees { get; set; }

        public virtual ICollection<Shift> Shifts { get; set; }

        public virtual ICollection<GroupEvents> Events { get; set; }
    }
}
