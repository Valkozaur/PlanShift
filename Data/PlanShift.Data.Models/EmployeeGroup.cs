﻿// ReSharper disable VirtualMemberCallInConstructor

using System.ComponentModel.DataAnnotations.Schema;

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class EmployeeGroup : BaseDeletableModel<string>
    {
        public EmployeeGroup()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Shifts = new HashSet<Shift>();
            this.CreatedShifts = new HashSet<Shift>();

            this.ChangedShifts = new HashSet<ShiftChange>();
            this.TakenShifts = new HashSet<ShiftChange>();
        }

        [Required]
        [Column("EmployeeId")]
        public string UserId { get; set; }

        public virtual PlanShiftUser User { get; set; }

        [Required]
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        [Required]
        [MaxLength(60)]
        public string Position { get; set; }

        public decimal Salary { get; set; }

        public bool IsManagement { get; set; }

        public virtual ICollection<Shift> Shifts { get; set; }

        public virtual ICollection<Shift> CreatedShifts { get; set; }

        public virtual ICollection<ShiftChange> ChangedShifts { get; set; }

        public virtual ICollection<ShiftChange> TakenShifts { get; set; }

        public virtual ICollection<ShiftChange> ManagedShifts { get; set; }
    }
}
