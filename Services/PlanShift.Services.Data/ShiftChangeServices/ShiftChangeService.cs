namespace PlanShift.Services.Data.ShiftChangeServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Models.Enumerations;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Services.Mapping;

    public class ShiftChangeService : IShiftChangeService
    {
        private readonly IDeletableEntityRepository<ShiftChange> shiftChangeRepository;
        private readonly IShiftService shiftService;

        public ShiftChangeService(IDeletableEntityRepository<ShiftChange> shiftChangeRepository, IShiftService shiftService)
        {
            this.shiftChangeRepository = shiftChangeRepository;
            this.shiftService = shiftService;
        }

        public async Task<string> Create(string shiftId, string originalEmployeeId, string candidateEmployeeId)
        {
            var shiftChange = new ShiftChange()
            {
                ShiftId = shiftId,
                OriginalEmployeeId = originalEmployeeId,
                PendingEmployeeId = candidateEmployeeId,
                IsAccepted = false,
            };

            await this.shiftService.StatusChange(shiftId, ShiftStatus.Pending);

            await this.shiftChangeRepository.AddAsync(shiftChange);
            await this.shiftChangeRepository.SaveChangesAsync();

            return shiftChange.Id;
        }

        public async Task<TViewModel> GetShiftChangeById<TViewModel>(string id)
            => await this.shiftChangeRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

        public async Task ApproveEmployeeForShift(string shiftChangeId, string managerId)
        {
            var shiftChange = await this.shiftChangeRepository.All().FirstOrDefaultAsync(x => x.Id == shiftChangeId);

            if (shiftChange == null)
            {
                throw new ArgumentNullException();
            }

            shiftChange.ManagementId = managerId;
            shiftChange.IsAccepted = true;

            await this.shiftService.ApproveShiftToEmployee(shiftChange.ShiftId, shiftChange.PendingEmployeeId, managerId);
        }

        public async Task DeclineShiftChange(string shiftChangeId, string managerId)
        {
            var shiftChange = await this.shiftChangeRepository.All().FirstOrDefaultAsync(x => x.Id == shiftChangeId);

            shiftChange.ManagementId = managerId;
            shiftChange.Shift.ShiftStatus = ShiftStatus.Accepted;

            await this.shiftChangeRepository.SaveChangesAsync();
        }
    }
}
