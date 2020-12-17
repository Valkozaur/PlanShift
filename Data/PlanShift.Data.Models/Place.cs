// ReSharper disable VirtualMemberCallInConstructor

namespace PlanShift.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class Place : BaseModel<int>
    {
        public Place()
        {
            this.Events = new HashSet<Event>();
            this.BusinessPlaces = new HashSet<BusinessPlaces>();
        }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<BusinessPlaces> BusinessPlaces { get; set; }
    }
}