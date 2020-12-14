namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Repositories;

    public abstract class BaseEntityBaseTestClass<T> : BaseTestClass
        where T : class
    {
        protected IRepository<T> GetMockedRepositoryWithCreateOperations(Mock<IRepository<T>> repository, List<T> fakeDb)
        {
            repository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate (T businessType)
                {
                    fakeDb.Add(businessType);
                });
            repository.Setup(r => r.SaveChangesAsync());

            return repository.Object;
        }

        protected IRepository<T> GetMockedRepositoryReturningAllAsNoTracking(Mock<IRepository<T>> repository, List<T> fakeDb)
        {
            var mockQueryable = fakeDb.AsQueryable().BuildMock();

            repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            return repository.Object;
        }

        protected IRepository<T> GetMockedRepositoryAll(Mock<IRepository<T>> repository, List<T> fakeDb)
        {
            var queryableMock = fakeDb.AsQueryable().BuildMock();
            repository.Setup(r => r.All())
                .Returns(queryableMock.Object);

            return repository.Object;
        }
    }
}