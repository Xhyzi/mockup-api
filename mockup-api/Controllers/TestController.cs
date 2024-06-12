using Microsoft.AspNetCore.Mvc;

namespace mockup_api.Controllers
{

    [Route("api/test")]
    [ApiController]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
