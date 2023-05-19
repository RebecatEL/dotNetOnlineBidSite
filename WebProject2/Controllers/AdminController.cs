using Microsoft.AspNetCore.Mvc;

namespace WebProject2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
