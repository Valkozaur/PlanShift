namespace PlanShift.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class GroupEvents : BaseModel<int>
    {
        [Required]
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}