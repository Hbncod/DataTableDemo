using Microsoft.AspNetCore.Mvc;

namespace JQueryDataTables.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
