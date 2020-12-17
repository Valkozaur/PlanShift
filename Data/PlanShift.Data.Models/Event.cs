// ReSharper disable VirtualMemberCallInConstructor

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class Event : BaseDeletableModel<string>
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Participants = new HashSet<EmployeeGroup>();
            this.Groups = new HashSet<GroupEvents>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public int PlaceId { get; set; }

        public string Description { get; set; }

        public virtual Place Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string CreatorId { get; set; }

        public EmployeeGroup Creator { get; set; }

        public virtual ICollection<EmployeeGroup> Participants { get; set; }

        public virtual ICollection<GroupEvents> Groups { get; set; }
    }
}
