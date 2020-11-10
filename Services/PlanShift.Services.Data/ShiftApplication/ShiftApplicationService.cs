namespace PlanShift.Services.Data.ShiftApplication
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;

    public class ShiftApplicationService : IShiftApplicationService
    {
        public ShiftApplicationService(IRepository<ShiftApplication> shiftApplicationRepository)
        {

        }

        public Task CreateShiftApplication(string shiftId, string employeeId)
        {
            var shiftApplication = new ShiftApplication()
            {
                ShiftId = shiftId,
                EmployeeId = employeeId,
                CreatedOn = DateTime.UtcNow,
            };
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllApplicationByShiftId<T>(string shiftId)
        {
            throw new System.NotImplementedException();
        }
    }
}
