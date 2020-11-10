namespace PlanShift.Web.ViewModels.Group
{
    using System.ComponentModel.DataAnnotations;

    public class GroupInputModel
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? StandardSalary { get; set; }

        public string BusinessName { get; set; }

        public string BusinessId { get; set; }
    }
}
