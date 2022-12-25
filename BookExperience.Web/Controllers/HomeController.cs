namespace BookExperience.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public IActionResult Index()
        => View();

        public IActionResult Error()
       => View();
    }

}
