using Microsoft.AspNetCore.Mvc;

namespace Fallah_App.Controllers
{
    public class ERROR404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
