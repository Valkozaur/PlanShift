namespace PlanShift.Services.Data.ShiftApplication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShiftApplicationService
    {
        Task<string> CreateShiftApplicationAsync(string shiftId, string employeeId);

        Task<bool> HasEmployeeAppliedForShift(string shiftId, string employeeId);

        Task ApproveShiftApplicationAsync(string id);

        Task<T> GetShiftApplicationById<T>(string id);

        Task<int> GetCountByBusinessIdAsync(string businessId);
    }
}
