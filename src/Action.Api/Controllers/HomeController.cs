using Microsoft.AspNetCore.Mvc;
namespace Action.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello From Action API");
    }
}