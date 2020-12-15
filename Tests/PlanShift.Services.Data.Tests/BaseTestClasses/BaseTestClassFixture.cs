namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Reflection;

    using PlanShift.Data.Common.Repositories;
    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;

    public abstract class BaseTestClassFixture
    {
        protected BaseTestClassFixture()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
