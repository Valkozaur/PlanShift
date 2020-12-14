namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;

    using PlanShift.Data.Common.Repositories;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.BusinessTypeServices;
    using PlanShift.Services.Data.Tests.BaseTestClasses;
    using PlanShift.Web.ViewModels.Business;

    using Xunit;

    public class BusinessTypeServiceTests : BaseEntityBaseTestClass<BusinessType>
    {
        private const string TestBusinessTypeName = "Test";

        private readonly Mock<IRepository<BusinessType>> Repository;
        private readonly List<BusinessType> fakeDb;
        private IBusinessTypeService businessTypeService;

        public BusinessTypeServiceTests()
        {
            this.fakeDb = new List<BusinessType>();
            this.Repository = new Mock<IRepository<BusinessType>>();
        }

        [Fact]
        public async Task CreateBusinessTypeShouldWorkCorrectly()
        {
            // Arrange
            this.businessTypeService = new BusinessTypeService(this.GetMockedRepositoryWithCreateOperations(this.Repository, this.fakeDb));

            // Act
            var id = await this.businessTypeService.CreateAsync(TestBusinessTypeName);

            // Assert
            Assert.Single(this.fakeDb);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnAllExistingTypes()
        {
            const int testBusinessTypesCount = 3;

            // Arrange
            this.fakeDb.Add(new BusinessType() { Id = 0, Name = TestBusinessTypeName });
            this.fakeDb.Add(new BusinessType() { Id = 1, Name = TestBusinessTypeName + 1 });
            this.fakeDb.Add(new BusinessType() { Id = 2, Name = TestBusinessTypeName + 2 });
  
            this.businessTypeService = new BusinessTypeService(this.GetMockedRepositoryReturningAllAsNoTracking(this.Repository, this.fakeDb));

            // Act
            var businessTypes = await this.businessTypeService.GetAllAsync<BusinessTypeDropDownViewModel>();

            // Assert
            Assert.Equal(testBusinessTypesCount, businessTypes.Count());
        }
    }
}
