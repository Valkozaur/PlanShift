// ReSharper disable VirtualMemberCallInConstructor

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PlanShift.Data.Common.Models;
    using PlanShift.Models.Enumerations;

    public class Shift : BaseDeletableModel<string>
    {
        public Shift()
        {
            this.Id = Guid.NewGuid().ToString();

            this.ShiftChanges = new HashSet<ShiftChange>();
        }

        public ShiftStatus ShiftStatus { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal BonusPayment { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        [Required]
        [ForeignKey(nameof(EmployeeGroup))]
        public string EmployeeId { get; set; }

        public virtual EmployeeGroup Employee { get; set; }

        [ForeignKey(nameof(EmployeeGroup))]
        public string ManagementId { get; set; }

        public virtual EmployeeGroup Management { get; set; }

        [Required]
        [ForeignKey(nameof(EmployeeGroup))]
        public string ShiftCreatorId { get; set; }

        public virtual EmployeeGroup ShiftCreator { get; set; }

        public virtual ICollection<ShiftChange> ShiftChanges { get; set; }
    }
}
