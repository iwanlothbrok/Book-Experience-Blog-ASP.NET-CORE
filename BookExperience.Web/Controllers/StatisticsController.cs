namespace BookExperience.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StatisticsController : Controller
    {
        public IActionResult All()
            => View();
    }
}
