using Microsoft.AspNetCore.Mvc;

namespace AppLinker.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{*path}", Order = int.MaxValue)]
        public ActionResult<string> Get() =>
            this.Request.Path.Value;
    }
}
