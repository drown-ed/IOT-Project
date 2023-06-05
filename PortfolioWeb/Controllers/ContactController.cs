using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using PortfolioWeb.Data;
using PortfolioWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace PortfolioWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WriteContact(Contact contact)
        {
            try
            {
                _db.Contact.Add(contact);
                _db.SaveChanges();

                Console.Write("전송 성공");
            } catch (Exception)
            {
                Console.Write("전송 실패");
            }

            return RedirectToAction("Index", "Contact");
        }
    }
}
