namespace PlanShift.Services.Data.ShiftApplication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Web.ViewModels.ShiftApplication;

    public interface IShiftApplicationService
    {
        Task<string> CreateShiftApplicationAsync(string shiftId, string employeeId);

        Task<bool> HasEmployeeAppliedForShift(string shiftId, string employeeId);

        Task<IEnumerable<T>> GetAllApplicationByShiftIdAsync<T>(string shiftId);

        Task ApproveShiftApplicationAsync(string id);

        Task<T> GetShiftApplicationById<T>(string id);

        Task<int> GetCountByBusinessIdAsync(string businessId);

        Task<IEnumerable<T>> GetAllActiveShiftApplicationsPerGroup<T>(string groupId);

        //Task<IEnumerable<T>> GetAllActiveShiftApplicationsPerBusiness<T>(string businessId);
    }
}
