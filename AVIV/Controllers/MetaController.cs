using AVIV.API.Api;
using Microsoft.AspNetCore.Mvc;

namespace AVIV.Controllers
{
    public class MetaController : BaseApiController
    {
        [HttpGet("/info")]
        public async Task<ActionResult<string>> Info() => "ok";
    }
}
