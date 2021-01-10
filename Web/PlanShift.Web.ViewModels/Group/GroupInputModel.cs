namespace PlanShift.Web.ViewModels.Group
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Web.Infrastructure.Validations.DataValidationAttributes;

    public class GroupInputModel
    {
        [Required]
        [MaxLength(120)]
        [GroupNameShouldNotBeOfficial]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? StandardSalary { get; set; }

        public string BusinessId { get; set; }

        public string BusinessName { get; set; }
    }
}
