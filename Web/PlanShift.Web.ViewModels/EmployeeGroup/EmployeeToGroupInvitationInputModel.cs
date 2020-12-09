namespace PlanShift.Web.ViewModels.EmployeeGroup
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Web.Infrastructure.Validations.DataValidationAttributes;

    public class EmployeeToGroupInvitationInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Email { get; set; }

        public string GroupId { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Position { get; set; }

        public bool IsManagement { get; set; }
    }
}
