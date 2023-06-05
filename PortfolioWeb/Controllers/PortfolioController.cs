using Microsoft.AspNetCore.Mvc;
using PortfolioWeb.Data;

namespace PortfolioWeb.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.Portfolios.ToList();
            return View(list);
        }
    }
}
