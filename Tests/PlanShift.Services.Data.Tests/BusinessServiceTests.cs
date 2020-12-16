namespace PlanShift.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.Business;

    using Xunit;

    public class BusinessServiceTests : DeletableEntityTestClass<Business>
    {
        private const string TestBusinessName = "Test";
        private const string TestUserId = "Test";
        private const int TestTypeId = 0;

        private IBusinessService businessService;

        [Fact]
        public async Task CreateBusinessAsyncShouldCreateNewBusinessInTheRepository()
        {
            // Arrange
            this.businessService = new BusinessService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var id = await this.businessService.CreateBusinessAsync(TestUserId, TestBusinessName, TestTypeId);

            // Assert
            Assert.NotNull(id);
            Assert.Contains(this.FakeDb, x => x.Name == TestBusinessName && x.OwnerId == TestUserId && x.BusinessTypeId == TestTypeId);
        }

        [Fact]
        public async Task GetAllForUserAsyncShouldReturnProperElement()
        {
            // Arrange
            this.FakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var businessesForUser = await this.businessService.GetAllForUserAsync<BusinessTestViewModel>(TestUserId);

            // Assert
            Assert.Single(businessesForUser);
            Assert.Contains(businessesForUser, b => b.Name == TestBusinessName);
        }

        [Fact]
        public async Task GetAllBusinessUserAsyncShouldReturnCountElementsWhenUserIsOwner()
        {
            const int countOfBusinesses = 2;

            // Arrange
            this.FakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.FakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = TestUserId, BusinessTypeId = TestTypeId + 1 });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var businessesForUser = await this.businessService.GetAllForUserAsync<BusinessTestViewModel>(TestUserId, 2);

            // Assert
            Assert.Equal(countOfBusinesses, businessesForUser.Count());
            Assert.Contains(businessesForUser, b => b.Name == TestBusinessName);
        }

        [Fact]
        public async Task GetAllBusinessesShouldNotReturnIfUserDoesNotHaveAnyBusinesses()
        {
            // Arrange
            var fakeUserId = Guid.NewGuid().ToString();

            this.FakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });
            this.FakeDb.Add(new Business() { Name = TestBusinessName + 1, OwnerId = TestUserId, BusinessTypeId = TestTypeId + 1 });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var businessesForUser = await this.businessService.GetAllForUserAsync<BusinessTestViewModel>(fakeUserId, 2);

            // Assert
            Assert.Empty(businessesForUser);
        }

        // TODO: Test if user is employee in Business
        [Fact]
        public async Task GetBusinessAsyncShouldReturnBusinessWhenGivenExistingId()
        {
            const string testBusinessId = "Test";

            // Arrange
            this.FakeDb.Add(new Business() { Id = testBusinessId,  Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());
            // Act
            var businesses = await this.businessService.GetBusinessAsync<BusinessTestViewModel>(testBusinessId);

            // Assert
            Assert.Equal(testBusinessId, businesses.Id);
            Assert.Equal(TestBusinessName, businesses.Name);
        }

        [Fact]
        public async Task GetBusinessAsyncShouldNotReturnBusinessWhenGivenNonExistingBusinessId()
        {
            const string nonExistingBusinessId = "Test";

            // Arrange

            this.FakeDb.Add(new Business() { Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var businesses = await this.businessService.GetBusinessAsync<BusinessTestViewModel>(nonExistingBusinessId);

            // Assert
            Assert.Null(businesses);
        }

        [Fact]
        public async Task GetOwnerIdAsyncShouldReturnOwnerIdWhenGivenExistingBusinessId()
        {
            const string testBusinessId = "Test";

            // Arrange
            this.FakeDb.Add(new Business() { Id = testBusinessId,  Name = TestBusinessName, OwnerId = TestUserId, BusinessTypeId = TestTypeId });

            this.businessService = new BusinessService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var ownerId = await this.businessService.GetOwnerIdAsync(testBusinessId);

            // Assert
            Assert.Equal(TestUserId, ownerId);
        }
    }
}
