namespace PlanShift.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using PlanShift.Data.Models;

    public class UsernameExistsAttribute : ValidationAttribute
    {
        public UsernameExistsAttribute()
        {
            this.ErrorMessage = "User with this username does not exist!";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<PlanShiftUser>)validationContext
                .GetService(typeof(UserManager<PlanShiftUser>));

            var user = userManager.Users.FirstOrDefault(x => x.UserName == value.ToString());

            if (user == null)
            {
                return null;
            }

            return ValidationResult.Success;
        }
    }
}
