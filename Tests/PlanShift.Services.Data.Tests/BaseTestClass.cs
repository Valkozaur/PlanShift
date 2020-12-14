using Moq;

namespace PlanShift.Services.Data.Tests
{
    using System.Reflection;

    using PlanShift.Services.Mapping;
    using PlanShift.Web.ViewModels;

    public abstract class BaseTestClass
    {
        public BaseTestClass()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
