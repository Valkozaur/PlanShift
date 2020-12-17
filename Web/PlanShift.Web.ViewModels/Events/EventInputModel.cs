namespace PlanShift.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class EventInputModel : IMapTo<Event>
    {
        [Required]
        [MinLength(5)]
        [MaxLength(80)]
        public string Name { get; set; }

        public int PlaceId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public IEnumerable<PlaceInfoViewModel> Places { get; set; }
    }
}