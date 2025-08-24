using Automacao.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Automacao.Features
{
    [Route("api/[controller]")]
    public class TesteController : ApiController
    {
        [HttpGet("lancar")]
        public IActionResult Pegar()
        {
            return ResponseOk();
        }
    }
}
