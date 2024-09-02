using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeopleApi.Controllers.Usuarios
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        /// <summary>
        /// Valida o acesso do usuário
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso efetuado com sucesso</response>
        /// <response code="404">Página não encontrada, caso existam dados inválidos</response>
        /// <response code="500">Caso ocorra erro interno do servidor</response>
        [HttpGet("api/acesso")]
        [Authorize(Policy = "IdadeMinima")]
        public IActionResult Get()
        {
            return Ok("Acesso permitido!");
        }
    }
}
