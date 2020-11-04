namespace PlanShift.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BusinessController : BaseController
    {
        public IActionResult Register()
        {
            return this.View();
        }
    }
}
