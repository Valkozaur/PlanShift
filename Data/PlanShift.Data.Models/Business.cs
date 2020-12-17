// ReSharper disable VirtualMemberCallInConstructor

namespace PlanShift.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class Business : BaseDeletableModel<string>
    {
        public Business()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Groups = new HashSet<Group>();
            this.Places = new HashSet<BusinessPlaces>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int BusinessTypeId { get; set; }

        public BusinessType BusinessType { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual PlanShiftUser Owner { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<BusinessPlaces> Places { get; set; }
    }
}
