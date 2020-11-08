namespace PlanShift.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EnsureStartDateIsBeforeEndDate : ValidationAttribute
    {
        private readonly DateTime endDate;

        public EnsureStartDateIsBeforeEndDate(DateTime endDate)
        {
            this.endDate = endDate;
        }

        public override bool IsValid(object value)
        {
            var startDate = (DateTime)value;
            if (startDate > endDate)
            {
                return false;
            }

            return true;
        }
    }
}
