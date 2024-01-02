using Microsoft.AspNetCore.Mvc;

namespace ShoppingProductApi.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
