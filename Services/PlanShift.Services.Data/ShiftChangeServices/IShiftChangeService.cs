namespace PlanShift.Services.Data.ShiftChangeServices
{
    using System.Threading.Tasks;

    public interface IShiftChangeService
    {
        Task<string> Create(string shiftId, string originalEmployeeId, string candidateEmployeeId);

        Task<T> GetShiftChangeById<T>(string id);

        Task ApproveEmployeeForShift(string shiftChangeId, string managerId);

        Task DeclineShiftChange(string shiftChangeId, string managerId);
    }
}
