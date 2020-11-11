namespace PlanShift.Services.Data.ShiftApplication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShiftApplicationService
    {
        Task<string> CreateShiftApplicationAsync(string shiftId, string employeeId);

        Task<IEnumerable<T>> GetAllApplicationByShiftIdAsync<T>(string shiftId);

        Task ApproveShiftApplicationAsync(string id);
    }
}
