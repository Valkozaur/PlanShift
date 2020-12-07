namespace PlanShift.Services.Data.ShiftChangeServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Models.Enumerations;

    public interface IShiftChangeService
    {
        Task<string> CreateShiftChangeAsync(string shiftId, string originalEmployeeId, string candidateEmployeeId);

        Task<T> GetShiftChangeById<T>(string id);

        Task<IEnumerable<T>> GetShiftChangesPerShift<T>(string shiftId);

        Task ProcessShiftChangeOriginalEmployeeStatus(string userId, string shiftChangeId, bool isAccepted);

        Task ApproveShiftChange(string shiftChangeId, string managerId);

        Task DeclineShiftChange(string shiftChangeId, string managerId);

        Task<int> GetCountByBusinessIdAsync(string businessId);

        Task<IEnumerable<T>> GetShiftChangesPerGroupAsync<T>(string groupId, ShiftApplicationStatus shiftApplicationStatus = ShiftApplicationStatus.Pending);
    }
}
