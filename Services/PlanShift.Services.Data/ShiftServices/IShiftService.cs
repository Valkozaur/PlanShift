namespace PlanShift.Services.Data.ShiftServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Models;
    using PlanShift.Models.Enumerations;

    public interface IShiftService
    {
        Task<string> CreateShift(string groupId, DateTime start, DateTime end, decimal bonusPayment = 0);

        Task DeleteShift(string shiftId);

        Task<TViewModel> GetShiftById<TViewModel>(string id);

        Task<ICollection<TViewModel>> GetAllShiftsByGroup<TViewModel>(string shiftId);

        Task StatusChange(string id, ShiftStatus newStatus);

        Task ApproveShiftToEmployee(string id, string employeeId, string managementId);
    }
}
