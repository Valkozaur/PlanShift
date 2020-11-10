﻿namespace PlanShift.Web.ViewModels.Business
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class BusinessTypeDropDownViewModel : IMapFrom<BusinessType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
