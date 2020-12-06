namespace PlanShift.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Data.Common.Models;

    public class InviteEmployeeVerifications : BaseModel<string>
    {
        public bool Used { get; set; }

        [Required]
        [MaxLength(80)]
        public string Email { get; set; }

        [Required]
        public string GroupId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Position { get; set; }

        public decimal Salary { get; set; }

    }
}