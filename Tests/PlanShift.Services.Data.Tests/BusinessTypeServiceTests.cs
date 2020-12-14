namespace PlanShift.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.Business;

    using Xunit;

    public class BusinessTypeServiceTests : BaseEntityBaseTestClass<BusinessType>
    {
        private const string TestBusinessTypeName = "Test";

        private IBusinessTypeService businessTypeService;

        [Fact]
        public async Task CreateBusinessTypeShouldWorkCorrectly()
        {
            // Arrange
            this.businessTypeService = new BusinessTypeService(this.GetMockedRepositoryWithCreateOperations());

            // Act
            var id = await this.businessTypeService.CreateAsync(TestBusinessTypeName);

            // Assert
            Assert.Single(this.FakeDb);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllExistingTypes()
        {
            const int testBusinessTypesCount = 3;

            // Arrange
            this.FakeDb.Add(new BusinessType() { Id = 0, Name = TestBusinessTypeName });
            this.FakeDb.Add(new BusinessType() { Id = 1, Name = TestBusinessTypeName + 1 });
            this.FakeDb.Add(new BusinessType() { Id = 2, Name = TestBusinessTypeName + 2 });

            this.businessTypeService = new BusinessTypeService(this.GetMockedRepositoryReturningAllAsNoTracking());

            // Act
            var businessTypes = await this.businessTypeService.GetAllAsync<BusinessTypeDropDownViewModel>();

            // Assert
            Assert.Equal(testBusinessTypesCount, businessTypes.Count());
        }
    }
}
