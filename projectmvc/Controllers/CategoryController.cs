using Microsoft.AspNetCore.Mvc;

namespace projectmvc.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
