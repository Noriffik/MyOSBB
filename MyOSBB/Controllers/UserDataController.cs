using Microsoft.AspNetCore.Mvc;

namespace MyOSBB.Controllers
{
    public class UserDataController : Controller
    {
        // Use JavaScript for display all invoices in one page
        public IActionResult Index()
        {
            return View();
        }
    }
}