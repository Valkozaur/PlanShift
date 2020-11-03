﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanShift.Data.Models
{
    using System;

    using PlanShift.Data.Common.Models;

    public class ShiftChange : BaseDeletableModel<string>
    {
        public ShiftChange()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool IsAccepted { get; set; }

        [Required]
        public string ShiftId { get; set; }

        public Shift Shift { get; set; }

        [Required]
        [ForeignKey(nameof(EmployeeGroup))]
        public string OriginalEmployeeId { get; set; }

        public EmployeeGroup OriginalEmployee { get; set; }

        [Required]
        [ForeignKey(nameof(EmployeeGroup))]
        public string PendingEmployeeId { get; set; }

        public virtual EmployeeGroup PendingEmployee { get; set; }

        [ForeignKey(nameof(EmployeeGroup))]
        public string ManagementId { get; set; }

        public virtual EmployeeGroup Management { get; set; }
    }
}
