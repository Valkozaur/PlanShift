namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MockQueryable.Moq;

    using PlanShift.Data.Models;
    using PlanShift.Services.Data.InvitationVerificationServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.InviteEmployeeValidation;
    using Xunit;

    public class InviteEmployeeVerificationServiceTests : BaseEntityBaseTestClass<InviteEmployeeVerification>, IClassFixture<AutoMapperFixture>
    {
        private const string GroupId = "Test";
        private const string Email = "Test";
        private const string Position = "Test";
        private const decimal Salary = 100M;

        private readonly AutoMapperFixture autoMapperFixture;
        private InviteEmployeeVerificationsService inviteEmployeeVerificationsService;

        public InviteEmployeeVerificationServiceTests(AutoMapperFixture autoMapperFixture)
        {
            this.autoMapperFixture = autoMapperFixture;
        }

        // TODO: Mock the object we are testing method.
        [Fact]
        public async Task CreateShiftVerificationAsyncShouldCreateEntityCorrectly()
        {
            // Arrange
            this.GetMockedRepositoryWithCreateOperations();

            var fakeList = new List<InviteEmployeeVerification>().AsQueryable().BuildMock();
            this.Repository.Setup(r => r.All())
                .Returns(fakeList.Object);

            this.inviteEmployeeVerificationsService = new InviteEmployeeVerificationsService(this.Repository.Object);

            // Act
            await this.inviteEmployeeVerificationsService.CreateShiftVerificationAsync(GroupId, Email, Position, Salary);

            // Assert
            Assert.Single(this.FakeDb);
            Assert.Contains(this.FakeDb, x => x.GroupId == GroupId && x.Email == Email);
        }

        [Fact]
        public async Task IsVerificationValidShouldChangeVerificationStatusToUsedAndReturnTrueIfThereIsValidVerification()
        {
            const string id = "Test";

            // Arrange
            var invitation = new InviteEmployeeVerification
            {
                Id = id,
                GroupId = GroupId,
                Email = Email,
                Position = Position,
                Salary = Salary,
            };
            this.FakeDb.Add(invitation);

            this.inviteEmployeeVerificationsService = new InviteEmployeeVerificationsService(this.GetMockedRepositoryAll());

            // Act
            var isValid = await this.inviteEmployeeVerificationsService.IsVerificationValid(id);

            // Assert
            Assert.True(isValid);
            Assert.True(invitation.Used);
        }

        [Fact]
        public async Task IsVerificationValidShouldReturnFalseWhenThereIsNoValidInvitation()
        {
            const string id = "Test";

            // Arrange
            var invitation1 = new InviteEmployeeVerification
            {
                Id = id,
                GroupId = GroupId,
                Email = Email,
                Position = Position,
                Salary = Salary,
                Used = true,
            };

            var invitation2 = new InviteEmployeeVerification
            {
                Id = id + 1,
                GroupId = GroupId,
                Email = Email,
                Position = Position,
                Salary = Salary,
                CreatedOn = DateTime.UtcNow.AddDays(-5),
            };

            this.FakeDb.Add(invitation1);
            this.FakeDb.Add(invitation2);

            this.inviteEmployeeVerificationsService = new InviteEmployeeVerificationsService(this.GetMockedRepositoryAll());

            // Act
            var isValid = await this.inviteEmployeeVerificationsService.IsVerificationValid(id);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public async Task GetVerificationAsyncShouldReturnVerificationWithId()
        {
            const string id = "Test";

            var invitation = new InviteEmployeeVerification
            {
                Id = id,
                GroupId = GroupId,
                Email = Email,
                Position = Position,
                Salary = Salary,
                Used = true,
            };

            this.FakeDb.Add(invitation);

            this.inviteEmployeeVerificationsService = new InviteEmployeeVerificationsService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var inviteEmployeeVerification = await this.inviteEmployeeVerificationsService.GetVerificationAsync<InviteEmployeeVerificationInfoViewModel>(id);

            // Assert
            Assert.NotNull(inviteEmployeeVerification);
            Assert.Equal(inviteEmployeeVerification.GroupId, GroupId);
            Assert.Equal(inviteEmployeeVerification.Email, Email);
            Assert.Equal(inviteEmployeeVerification.Position, Position);
            Assert.Equal(inviteEmployeeVerification.Salary, Salary);
        }

        [Fact]
        public async Task GetVerificationAsyncShouldReturnNothingWhenThereIsNoSuchVerification()
        {
            const string id = "Test";
            const string fakeId = "Fake";

            var invitation = new InviteEmployeeVerification
            {
                Id = id,
                GroupId = GroupId,
                Email = Email,
                Position = Position,
                Salary = Salary,
                Used = true,
            };

            this.FakeDb.Add(invitation);

            this.inviteEmployeeVerificationsService = new InviteEmployeeVerificationsService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var inviteEmployeeVerification = await this.inviteEmployeeVerificationsService.GetVerificationAsync<InviteEmployeeVerificationInfoViewModel>(fakeId);

            // Assert
            Assert.Null(inviteEmployeeVerification);
        }
    }
}
