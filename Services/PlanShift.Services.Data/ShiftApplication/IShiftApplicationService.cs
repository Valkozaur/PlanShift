using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanShift.Services.Data.ShiftApplication
{
    public interface IShiftApplicationService
    {
        Task CreateShiftApplication(string shiftId, string employeeId);

        Task<IEnumerable<T>> GetAllApplicationByShiftId<T>(string shiftId);
    }
}
