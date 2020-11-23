namespace PlanShift.Web.ViewModels.Shift
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Web.Infrastructure.Validations.ValidationAttributes;

    public class CreateShiftInputModel
    {
        [DateIsFuture]
        public DateTime Start { get; set; }

        [DateIsFuture]
        public DateTime End { get; set; }

        [Required]
        public string GroupId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? BonusPayment { get; set; }
    }
}
