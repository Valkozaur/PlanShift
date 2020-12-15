using System;
using System.Reflection;
using System.Threading.Tasks;
using PlanShift.Data.Models.Enumerations;
using PlanShift.Services.Mapping;
using PlanShift.Web.ViewModels;
using PlanShift.Web.ViewModels.Shift;
using Xunit;

namespace PlanShift.Services.Data.Tests
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.ShiftServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;

    public class ShiftServiceTests : IClassFixture<DeletableEntityBaseTestClassFixture<Shift>>
    {
        private const string ShiftCreatorId = "Test";
        private const string GroupId = "Test";
        private const string Description = "Test";
        private const decimal BonusPayment = 100M;

        private readonly DateTime start;
        private readonly DateTime end;

        private readonly DeletableEntityBaseTestClassFixture<Shift> classFixture;

        private IShiftService shiftService;

        public ShiftServiceTests(DeletableEntityBaseTestClassFixture<Shift> classFixture)
        {
            this.classFixture = classFixture;
            this.start = DateTime.UtcNow.AddDays(1);
            this.end = this.start.AddHours(8);
        }

        [Fact]
        public async Task CreateShiftShouldCreateAndReturnStringIdIfEverythingIsCorrect()
        {
            // Arrange
            this.shiftService = new ShiftService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var shiftId = await this.shiftService.CreateShift(ShiftCreatorId, GroupId, this.start, this.end, Description, BonusPayment);

            // Assert
            Assert.NotNull(shiftId);
            Assert.Single(this.FakeDb);
        }

        [Fact]
        public async Task ApproveShiftToEmployeeShouldChangeShiftStatusToApproved()
        {
            const string shiftId = "Test";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryAll());

            // Act
            await this.shiftService.ApproveShiftToEmployee(shiftId, ShiftCreatorId, ShiftCreatorId);

            // Assert
            Assert.Equal(ShiftStatus.Approved, shift.ShiftStatus);
            Assert.Equal(ShiftCreatorId, shift.EmployeeId);
        }

        [Fact]
        public void ApproveShiftToEmployeeShouldThrowIfNoShiftWasFound()
        {
            const string shiftId = "Test";
            const string fakeShiftId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryAll());

            // Act
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => this.shiftService.ApproveShiftToEmployee(fakeShiftId, ShiftCreatorId, ShiftCreatorId));
        }

        [Fact]
        public async Task GetShiftByIdShouldReturnShiftIfExisting()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            const string shiftId = "Test";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftResult = await this.shiftService.GetShiftById<ShiftTestViewModel>(shiftId);

            // Assert
            Assert.NotNull(shiftResult);
            Assert.Equal(shiftId, shiftResult.Id);
        }

        [Fact]
        public async Task GetShiftByIdShouldReturnNothingIfShiftDoesNotExist()
        {
            const string shiftId = "Test";
            const string fakeShiftId = "Fake";

            // Arrange
            var shift = new Shift()
            {
                Id = shiftId,
                Start = this.start,
                End = this.end,
                ShiftCreatorId = ShiftCreatorId,
                GroupId = GroupId,
                Description = Description,
                BonusPayment = BonusPayment,
                ShiftStatus = ShiftStatus.Open,
            };

            this.FakeDb.Add(shift);

            this.shiftService = new ShiftService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var shiftResult = await this.shiftService.GetShiftById<ShiftTestViewModel>(fakeShiftId);

            // Assert
            Assert.Null(shiftResult);
        }
    }
}
