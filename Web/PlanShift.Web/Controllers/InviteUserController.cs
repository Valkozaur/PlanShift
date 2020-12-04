namespace PlanShift.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class InviteUserController : Controller
    {
        public IActionResult Index()
        {
            //TODO: Implement send invitation email with SendGrid;

            return View();
        }
    }
}
