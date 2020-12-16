namespace PlanShift.Services.Data.ShiftServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Models.Enumerations;

    public interface IShiftService
    {
        Task<string> CreateShiftAsync(string shiftCreatorId, string groupId, DateTime start, DateTime end, string description, decimal bonusPayment = 0);

        //Task DeleteShift(string shiftId);

        Task<T> GetShiftByIdAsync<T>(string id);

        Task<ICollection<T>> GetAllShiftsByGroupAsync<T>(string shiftId);

        Task StatusChangeAsync(string id, ShiftStatus newStatus);

        Task ApproveShiftToEmployeeAsync(string id, string employeeId, string managementId);

        Task<IEnumerable<T>> GetPendingShiftsPerGroupAsync<T>(string groupId);

        Task<IEnumerable<T>> GetUpcomingShiftForUserAsync<T>(string businessId, string userId);

        Task<IEnumerable<T>> GetOpenShiftsAvailableForUserAsync<T>(string businessId, string userId);

        Task<IEnumerable<T>> GetUsersShiftsWithDeclaredSwapRequestsAsync<T>(string businessId, string userId);

        Task<IEnumerable<T>> GetTakenShiftsPerUserAsync<T>(string businessId, string userId);
    }
}
