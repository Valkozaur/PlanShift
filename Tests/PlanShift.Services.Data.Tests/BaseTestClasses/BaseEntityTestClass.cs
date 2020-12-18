namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using MockQueryable.Moq;
    using Moq;
    using PlanShift.Data.Common.Repositories;

    public class BaseEntityTestClass<T> : BaseTestClass
        where T : class
    {
        protected BaseEntityTestClass()
        {
            this.Repository = new Mock<IRepository<T>>();
            this.FakeDb = new List<T>();
        }

        protected Mock<IRepository<T>> Repository { get; set; }

        protected List<T> FakeDb { get; set; }

        protected IRepository<T> GetMockedRepositoryWithCreateOperations()
        {
            this.Repository.Setup(r => r.AddAsync(It.IsAny<T>()))
                .Callback(delegate(T businessType)
                {
                    this.FakeDb.Add(businessType);
                });
            this.Repository.Setup(r => r.SaveChangesAsync());

            return this.Repository.Object;
        }

        protected IRepository<T> GetMockedRepositoryReturningAllAsNoTracking()
        {
            var mockQueryable = this.FakeDb.AsQueryable().BuildMock();

            this.Repository.Setup(r => r.AllAsNoTracking())
                .Returns(mockQueryable.Object);

            return this.Repository.Object;
        }

        protected IRepository<T> GetMockedRepositoryAll()
        {
            var queryableMock = this.FakeDb.AsQueryable().BuildMock();
            this.Repository.Setup(r => r.All())
                .Returns(queryableMock.Object);

            return this.Repository.Object;
        }
    }
}
