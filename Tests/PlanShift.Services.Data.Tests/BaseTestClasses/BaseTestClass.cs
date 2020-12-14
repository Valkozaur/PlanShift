namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Reflection;

    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;

    public abstract class BaseTestClass
    {
        protected BaseTestClass()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
