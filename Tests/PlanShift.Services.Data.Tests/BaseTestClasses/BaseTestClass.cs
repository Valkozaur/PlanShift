namespace PlanShift.Services.Data.Tests.BaseTestClasses
{
    using System.Reflection;

    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;

    public class BaseTestClass
    {
        public BaseTestClass()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
