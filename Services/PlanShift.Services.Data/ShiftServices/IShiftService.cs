namespace PlanShift.Services.Data.ShiftServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Models.Enumerations;

    public interface IShiftService
    {
        Task<string> CreateShift(string shiftCreatorId, string groupId, DateTime start, DateTime end, string description, decimal bonusPayment = 0);

        Task DeleteShift(string shiftId);

        Task<T> GetShiftById<T>(string id);

        Task<ICollection<T>> GetAllShiftsByGroup<T>(string shiftId);

        Task StatusChange(string id, ShiftStatus newStatus);

        Task ApproveShiftToEmployee(string id, string employeeId, string managementId);

        Task<string> GetGroupIdAsync(string shiftId);

        Task<IEnumerable<T>> GetPendingShiftsPerGroup<T>(string groupId);

        Task<IEnumerable<T>> GetUpcomingShiftForUser<T>(string businessId, string userId);
    }
}
