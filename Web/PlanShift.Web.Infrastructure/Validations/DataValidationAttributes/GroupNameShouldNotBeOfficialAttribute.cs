namespace PlanShift.Web.Infrastructure.Validations.DataValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using PlanShift.Common;

    public class GroupNameShouldNotBeOfficialAttribute : ValidationAttribute
    {
        public GroupNameShouldNotBeOfficialAttribute()
        {
            this.ErrorMessage = "User with this username does not exist!";
        }

        public override bool IsValid(object value)
        {
            var groupName = value.ToString();

            var isNameOfficial = groupName == GlobalConstants.AdminsGroupName || groupName == GlobalConstants.HrGroupName || groupName == GlobalConstants.ScheduleManagersGroupName;

            if (isNameOfficial)
            {
                return false;
            }

            return true;
        }
    }
}