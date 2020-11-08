using Microsoft.AspNetCore.Mvc;

namespace PlanShift.Web.Controllers
{
    public class ShiftController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
