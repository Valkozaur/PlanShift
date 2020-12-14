namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Moq;
    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Data.Models.Enumerations;
    using PlanShift.Services.Data.ShiftApplicationServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.ShiftApplication;
    using Xunit;

    public class ShiftApplicationServiceTests : BaseEntityBaseTestClass<ShiftApplication>
    {
        private const string ShiftId = "Test";
        private const string EmployeeId = "Test";

        private IShiftApplicationService shiftApplicationService;

        [Fact]
        public async Task CreateShiftApplicationAsyncCreateEntityIfGivenCorrectItems()
        {
            // Arrange
            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var shiftApplicationId = await this.shiftApplicationService.CreateShiftApplicationAsync(ShiftId, EmployeeId);

            // Assert
            Assert.NotNull(this.shiftApplicationService);
            Assert.Single(this.FakeDb);
            Assert.Contains(this.FakeDb, application => application.EmployeeId == EmployeeId && application.ShiftId == ShiftId);
        }

        [Fact]
        public async Task HasEmployeeActiveApplicationForShiftAsyncShouldReturnTrueIfThereIsSuchApplicationInTheDb()
        {
            // Arrange
            var shiftApplication = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeActiveApplicationForShiftAsync(ShiftId, EmployeeId);

            // Assert
            Assert.True(hasEmployeeApplied);
        }

        [Fact]
        public async Task HasEmployeeActiveApplicationForShiftAsyncShouldReturnTrueIfThereIsNoSuchShiftApplicationInTheDb()
        {
            const string fakeUserId = "Fake";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeActiveApplicationForShiftAsync(ShiftId, fakeUserId);

            // Assert
            Assert.False(hasEmployeeApplied);
        }

        [Fact]
        public async Task HasEmployeeActiveApplicationForShiftAsyncShouldReturnTrueIfThereIsNoSuchActiveShiftApplication()
        {
            // Arrange
            var shiftApplication = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Unknown,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var hasEmployeeApplied = await this.shiftApplicationService.HasEmployeeActiveApplicationForShiftAsync(ShiftId, EmployeeId);

            // Assert
            Assert.False(hasEmployeeApplied);
        }

        [Fact]
        public async Task ApproveShiftApplicationShouldChangeTheStatusOfTheApplicationIfEverythingIsCorrect()
        {
            const string id = "Test";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                Id = id,
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftApplicationService.ApproveShiftApplicationAsync(id);

            // Assert
            Assert.True(shiftApplication.Status == ShiftApplicationStatus.Approved);
        }

        [Fact]
        public void ApproveShiftApplicationShouldThrowIfGivenApplicationDoesNotExist()
        {
            const string id = "Test";

            // Arrange
            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftApplicationService.ApproveShiftApplicationAsync(id));
        }

        [Fact]
        public async Task DeclineAllShiftApplicationsPerShiftAsyncShouldChangeAllShiftApplicationShiftStatusToDeclined()
        {
            const string id = "Test";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                Id = id,
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftApplicationService.DeclineAllShiftApplicationsPerShiftAsync(ShiftId);

            // Assert
            Assert.True(shiftApplication.Status == ShiftApplicationStatus.Declined);
        }

        [Fact]
        public void DeclineAllShiftApplicationsPerShiftAsyncShouldThrowIfNoShiftsApplicationsForShiftFound()
        {
            const string fakeShiftId = "Fake";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                ShiftId = fakeShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftApplicationService.DeclineAllShiftApplicationsPerShiftAsync(ShiftId));
        }

        [Fact]
        public async Task GetShiftApplicationByIdShouldReturnShiftApplicationIfSuchExists()
        {
            const string id = "Test";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                Id = id,
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,

            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var model = await this.shiftApplicationService.GetShiftApplicationById<ShiftApplicationTestViewModel>(id);

            // Assert
            Assert.NotNull(model);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task GetShiftApplicationByIdShouldNotReturnShiftApplicationIfNoSuchExists()
        {
            const string id = "Test";
            const string fakeId = "Fake";

            // Arrange
            var shiftApplication = new ShiftApplication
            {
                Id = id,
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
            };

            this.FakeDb.Add(shiftApplication);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var model = await this.shiftApplicationService.GetShiftApplicationById<ShiftApplicationTestViewModel>(fakeId);

            // Assert
            Assert.Null(model);
        }

        [Fact]
        public async Task GetCountOfPendingApplicationsByBusinessIdAsyncShouldReturnProperCountIfPendingApplicationExist()
        {
            const int expectedCount = 2;
            const string businessId = "Test";

            // Model
            var shift = new Shift()
            {
                Group = new Group() { BusinessId = businessId },
            };

            var shiftApplication = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };

            var shiftApplication2 = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };

            this.FakeDb.Add(shiftApplication);
            this.FakeDb.Add(shiftApplication2);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var count = await this.shiftApplicationService.GetCountOfPendingApplicationsByBusinessIdAsync(businessId);

            // Assert
            Assert.Equal(expectedCount, count);
        }

        [Fact]
        public async Task GetCountOfPendingApplicationsByBusinessIdAsyncShouldNotReturnProperCountIfPendingApplicationDoesNotExist()
        {
            const int expectedCount = 0;
            const string businessId = "Test";
            const string fakeBusinessId = "Fake";

            // Model
            var shift = new Shift()
            {
                Group = new Group() { BusinessId = businessId },
            };

            var shiftApplication = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };

            var shiftApplication2 = new ShiftApplication
            {
                ShiftId = ShiftId,
                EmployeeId = EmployeeId,
                CreatedOn = DateTime.UtcNow,
                Status = ShiftApplicationStatus.Pending,
                Shift = shift,
            };

            this.FakeDb.Add(shiftApplication);
            this.FakeDb.Add(shiftApplication2);

            this.shiftApplicationService = new ShiftApplicationService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var count = await this.shiftApplicationService.GetCountOfPendingApplicationsByBusinessIdAsync(fakeBusinessId);

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}
