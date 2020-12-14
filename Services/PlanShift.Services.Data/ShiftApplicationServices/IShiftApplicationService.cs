namespace PlanShift.Services.Data.ShiftApplicationServices
{
    using System.Threading.Tasks;

    public interface IShiftApplicationService
    {
        Task<string> CreateShiftApplicationAsync(string shiftId, string employeeId);

        Task<bool> HasEmployeeActiveApplicationForShiftAsync(string shiftId, string employeeId);

        Task ApproveShiftApplicationAsync(string id);

        Task DeclineAllShiftApplicationsPerShiftAsync(string shiftId);

        Task<T> GetShiftApplicationById<T>(string id);

        Task<int> GetCountOfPendingApplicationsByBusinessIdAsync(string businessId);
    }
}
