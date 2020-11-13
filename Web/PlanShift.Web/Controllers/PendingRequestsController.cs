using Microsoft.AspNetCore.Mvc;

namespace PlanShift.Web.Controllers
{
    public class PendingRequestsController : Controller
    {
        public IActionResult AllRequests()
        {
            return this.View();
        }
    }
}
