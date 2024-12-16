using Microsoft.AspNetCore.Mvc;

namespace SORAPC.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Catalog()
        {
            return View();
        }
    }
}