using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
