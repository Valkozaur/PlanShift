using Microsoft.AspNetCore.Mvc;

namespace PlanShift.Web.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
