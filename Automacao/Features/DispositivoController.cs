using Automacao.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Automacao.Features
{
    [Route("api/[controller]")]
    public class DispositivoController : ApiController
    {
        [HttpGet("pegar")]
        public IActionResult Pegar()
        {
            return ResponseOk();
        }
    }
}
