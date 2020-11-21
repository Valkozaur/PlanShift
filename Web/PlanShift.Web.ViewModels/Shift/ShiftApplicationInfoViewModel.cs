namespace PlanShift.Web.ViewModels.Shift
{
    using PlanShift.Data.Models;
    using PlanShift.Services.Mapping;

    public class ShiftApplicationInfoViewModel : IMapFrom<ShiftApplication>
    {
        public string Id { get; set; }

        public string EmployeeEmployeeUsername { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<ShiftApplication, ShiftApplicationInfoViewModel>()
        //        .ForMember(
        //            m => m.EmployeeName,
        //            sa 
        //                => sa.MapFrom(x => x.Employee.Employee.UserName));
        //}
    }
}
