using CityInfo.API.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/testdatabase")]
    public class dummyController : ControllerBase
    {
        private readonly CityInfoContext ctx;

        public dummyController(CityInfoContext ctx)
        {
            this.ctx = ctx;
        }
        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
