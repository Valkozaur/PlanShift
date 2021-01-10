// ReSharper disable VirtualMemberCallInConstructor

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 

    using PlanShift.Data.Common.Models;

    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Guests = new HashSet<EmployeeGroupEvent>();
        }

        [Required] [MaxLength(80)] public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public bool IsInvitationOnly { get; set; }

        [Required] public string CreatedById { get; set; }

        public virtual EmployeeGroup CreatedBy { get; set; }

        [Required]
        public string BusinessId { get; set; }

        public Business Business { get; set; }

        public virtual ICollection<EmployeeGroupEvent> Guests { get; set; }

        public virtual ICollection<GroupEvents> Groups { get; set; }
    }
}