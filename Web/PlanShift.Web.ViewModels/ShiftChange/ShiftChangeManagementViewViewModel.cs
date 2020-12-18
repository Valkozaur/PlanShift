namespace PlanShift.Web.ViewModels.ShiftChange
{
    using System;

    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftChangeManagementViewViewModel : ShiftChangeUserViewModel, IMapFrom<ShiftChange>
    {
        public string OriginalEmployeeUserFullName { get; set; }
    }
}
