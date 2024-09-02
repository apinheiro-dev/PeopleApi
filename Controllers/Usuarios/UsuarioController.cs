using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data.Dtos.Usuarios;
using PeopleApi.Services.Usuarios;

namespace PeopleApi.Controllers.Usuarios
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        /// <summary>
        /// Adiciona um usuário ao banco de dados
        /// </summary>
        /// <param name="usuarioDto">Objeto com os campos necessários para criação de um usuário</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        /// <response code="400">Caso existam dados inválidos</response>
        /// <response code="500">Caso ocorra erro interno do servidor</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarUsuarioAsync(CreateUsuarioDto usuarioDto)
        {
            await _usuarioService.CadastrarUsuarioAsync(usuarioDto);
            return Ok("Usuário cadastrado!");

        }

        /// <summary>
        /// Efetua o login de um usuário cadastrado
        /// </summary>
        /// <param name="usuarioDto">Objeto com os campos necessários para criação de um usuário</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso efetuado com sucesso</response>
        /// <response code="400">Caso existam dados inválidos</response>
        /// <response code="500">Caso ocorra erro interno do servidor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto usuarioDto)
        {
            var token = await _usuarioService.LoginAsync(usuarioDto);
            return Ok(token);
        }


        /// <summary>
        /// Busca um usuário cadastrado pelo id
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será removido</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso efetuado com sucesso</response>
        /// <response code="400">Caso existam dados inválidos</response>
        /// <response code="500">Caso ocorra erro interno do servidor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("buscarusuarioporid")]
        public IActionResult GetUsuarioPorId(string id)
        {
            _usuarioService.GetUsuarioPorId(id);
            return Ok("Usuário cadastrado!");

        }

        

        /// <summary>
        /// Remove um usuário do banco de dados
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será removido</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso remoção seja feita com sucesso (sem conteúdo)</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("removerusuario")]
        public IActionResult RemoverUsuario(string id)
        {
            //await _usuarioService.CadastrarUsuarioAsync(usuarioDto);
            _usuarioService.RemoverUsuario(id);
            return NoContent();

        }



    }
}
