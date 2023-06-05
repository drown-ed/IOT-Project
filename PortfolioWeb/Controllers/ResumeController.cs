using Microsoft.AspNetCore.Mvc;

namespace PortfolioWeb.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
