namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration.UserSecrets;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.ShiftChangeServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.ShiftChange;
    using Xunit;

    public class ShiftChangeServiceTests : BaseEntityTestClass<ShiftChange>
    {
        private const string ShiftId = "Test";
        private const string OriginalEmployeeId = "Test";
        private const string PendingEmployeeId = "Test";

        private IShiftChangeService shiftChangeService;

        [Fact]
        public async Task CreateShouldWorkCorrectlyIfGivenProperInformation()
        {
            // Arrange
            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var shiftChangeId = await this.shiftChangeService.CreateShiftChangeAsync(ShiftId, OriginalEmployeeId, PendingEmployeeId);

            // Assert
            Assert.NotNull(shiftChangeId);
            Assert.Single(this.FakeDb);
            Assert.Contains(this.FakeDb, s => s.Id == shiftChangeId);
        }

        [Fact]
        public async Task AcceptShiftChangeByOriginalEmployeeAsyncShouldWorkCorrectlyIfGivenProperParameters()
        {
            const string id = "Test";

            // Arrange
            var originalEmployee = new EmployeeGroup { UserId = OriginalEmployeeId };

            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployee = originalEmployee,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftChangeService.AcceptShiftChangeByOriginalEmployeeAsync(OriginalEmployeeId, id, true);

            // Assert
            Assert.Contains(this.FakeDb, sc => sc.Id == id && sc.IsApprovedByOriginalEmployee == true);
        }

        [Fact]
        public void  AcceptShiftChangeByOriginalEmployeeAsyncShouldThrowIfChangeIsAcceptedByDifferentEmployee()
        {
            const string id = "Test";
            const string fakeOriginalEmployeeId = "Fake";

            // Arrange
            var originalEmployee = new EmployeeGroup { UserId = OriginalEmployeeId };

            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployee = originalEmployee,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftChangeService.AcceptShiftChangeByOriginalEmployeeAsync(fakeOriginalEmployeeId, id, true));
        }

        [Fact]
        public void AcceptShiftChangeByOriginalEmployeeAsyncShouldThrowIfNoSuchShiftFound()
        {
            const string id = "Test";
            const string fakeId = "Fake";

            // Arrange
            var originalEmployee = new EmployeeGroup { UserId = OriginalEmployeeId };

            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployee = originalEmployee,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftChangeService.AcceptShiftChangeByOriginalEmployeeAsync(OriginalEmployeeId, fakeId, true));
        }

        [Fact]
        public async Task ApproveShiftChangeAsyncShouldShiftChangeAndChangeItsStatus()
        {
            const string id = "Test";
            const string managerId = "Test";

            // Arrange
            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftChangeService.ApproveShiftChangeAsync(id, managerId);

            // Assert
            Assert.Contains(this.FakeDb, sc => sc.Id == id && sc.Status == ShiftApplicationStatus.Approved);
        }

        [Fact]
        public async Task GetShiftChangeByIdAsyncShouldReturnEntityWhenGivenExistingId()
        {
            const string id = "Test";

            // Arrange
            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChangeModel = await this.shiftChangeService.GetShiftChangeByIdAsync<ShiftChangeInfoViewModel>(id);

            // Assert
            Assert.NotNull(shiftChangeModel);
            Assert.Equal(shiftChangeModel.PendingEmployeeId, PendingEmployeeId);
            Assert.Equal(shiftChangeModel.ShiftId, ShiftId);
        }

        [Fact]
        public async Task GetShiftChangeByIdAsyncShouldNotReturnIfShiftWithThisIdDoesNotExists()
        {
            const string id = "Test";
            const string fakeId = "Fake";

            // Arrange
            var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChangeModel = await this.shiftChangeService.GetShiftChangeByIdAsync<ShiftChangeInfoViewModel>(fakeId);

            // Assert
            Assert.Null(shiftChangeModel);
        }

        [Fact]
        public async Task GetShiftChangesPerShiftAsyncShouldReturnAllShiftsChangesForGivenShiftId()
        {
            const string id = "Test";
            const string groupName = "Test";

            // Arrange
            var group = new Group() { Name = groupName };
            var shift = new Shift() { Start = DateTime.UtcNow, Group = group};

        var shiftChange = new ShiftChange()
            {
                Id = id,
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
                IsApprovedByOriginalEmployee = true,
                Shift = shift,
        };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChangeModels = await this.shiftChangeService.GetShiftChangesPerShiftAsync<ShiftChangeInfoViewModel>(ShiftId);

            // Assert
            Assert.Single(shiftChangeModels);
        }

        [Fact]
        public async Task GetShiftChangesPerShiftAsyncShouldReturnNoShiftChangesIfShiftDoesntHaveAny()
        {
            const string fakeShiftId = "Fake";

            // Arrange
            var shiftChange = new ShiftChange()
            {
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChangeModels = await this.shiftChangeService.GetShiftChangesPerShiftAsync<ShiftChangeInfoViewModel>(fakeShiftId);

            // Assert
            Assert.Empty(shiftChangeModels);
        }

        [Fact]
        public async Task GetCountByBusinessIdAsyncShouldReturnTheCountOfAllShiftChangesPerBusiness()
        {
            const int expectedCount = 1;
            const string businessId = "Test";

            // Arrange
            var group = new Group() { BusinessId = businessId };
            var shift = new Shift() { Group = group };

            var shiftChange = new ShiftChange()
            {
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
                IsApprovedByOriginalEmployee = true,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var count = await this.shiftChangeService.GetCountByBusinessIdAsync(businessId);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task GetCountByBusinessIdAsyncShouldReturnZeroIfNoShiftChangesPerBusiness()
        {
            const int expectedCount = 0;
            const string businessId = "Test";
            const string fakeBusinessId = "Test";

            // Arrange
            var group = new Group() { BusinessId = businessId };
            var shift = new Shift() { Group = group };

            var shiftChange = new ShiftChange()
            {
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var count = await this.shiftChangeService.GetCountByBusinessIdAsync(fakeBusinessId);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task GetShiftChangesPerGroupAsyncShouldReturnAllShiftForGivenGroup()
        {
            const string groupId = "Test";

            // Arrange
            var shift = new Shift() { GroupId = groupId };

            var shiftChange = new ShiftChange()
            {
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
                IsApprovedByOriginalEmployee = true,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChanges = await this.shiftChangeService.GetShiftChangesPerGroupAsync<ShiftChangeInfoViewModel>(groupId);

            // Assert
            Assert.Single(shiftChanges);
        }

        [Fact]
        public async Task GetShiftChangesPerGroupAsyncShouldNotReturnAnyChangesIfSuchDoNotExistsForTheGivenGroup()
        {
            const string groupId = "Test";
            const string fakeGroupId = "Test";

            // Arrange
            var shift = new Shift() { GroupId = groupId };

            var shiftChange = new ShiftChange()
            {
                ShiftId = ShiftId,
                OriginalEmployeeId = OriginalEmployeeId,
                PendingEmployeeId = PendingEmployeeId,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };
            this.FakeDb.Add(shiftChange);

            this.shiftChangeService = new ShiftChangeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftChanges = await this.shiftChangeService.GetShiftChangesPerGroupAsync<ShiftChangeInfoViewModel>(fakeGroupId);

            // Assert
            Assert.Empty(shiftChanges);
        }
    }
}
