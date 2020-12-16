namespace PlanShift.Services.Data.ShiftChangeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Models.Enumerations;

    public interface IShiftChangeService
    {
        Task<string> CreateShiftChangeAsync(string shiftId, string originalEmployeeId, string candidateEmployeeId);

        Task AcceptShiftChangeByOriginalEmployeeAsync(string userId, string shiftChangeId, bool isAccepted);

        Task ApproveShiftChangeAsync(string shiftChangeId, string managerId);

        Task<T> GetShiftChangeByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetShiftChangesPerShiftAsync<T>(string shiftId);

        Task<int> GetCountByBusinessIdAsync(string businessId);

        Task<IEnumerable<T>> GetShiftChangesPerGroupAsync<T>(string groupId, ShiftApplicationStatus shiftApplicationStatus = ShiftApplicationStatus.Pending);
    }
}
