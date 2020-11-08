﻿namespace PlanShift.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateIsFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (date < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    }
}