namespace PlanShift.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Repositories;

    public abstract class BaseEntityBaseTestClass : BaseTestClass
    {
        protected BaseEntityBaseTestClass()
        {
        }

        protected void SetMockedRepositoryCreateOperations<T>(Mock<IRepository<T>> repository, List<T> fakeDb)
            where T : class
        {
            repository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate (T businessType)
                {
                    fakeDb.Add(businessType);
                });
            repository.Setup(r => r.SaveChangesAsync());
        }

        protected void SetMockedRepositoryReturningAllAsNoTracking<T>(Mock<IRepository<T>> repository, List<T> fakeDb)
            where T : class
        {
            var mockQueryable = fakeDb.AsQueryable().BuildMock();

            repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);
        }
    }
}