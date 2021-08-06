using Microsoft.AspNetCore.Mvc;

namespace HeartInsurance.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Users() => View();
        public IActionResult UsersDetails() => View();
    }
}
