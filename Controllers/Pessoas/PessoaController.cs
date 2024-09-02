using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Data.Dtos.Pessoas;
using PeopleApi.Data.Dtos.Usuarios;
using PeopleApi.Models;
using PeopleApi.Services.Usuarios;

namespace PeopleApi.Controllers.Pessoas
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api")]
    public class PessoaController : ControllerBase
    {
        /*
         Cria Pessoa                                Post
         Leitura Pessoa - Não retornar foto         Get
        Atualiza Pessoa                             Put
        Delete Pessoa                               Delete
        Pesquisa Pessoa por: Nome, CPF, Data Nascimento e Sexo      Get
        Busca Foto Pelo     Id     da Pessoa                        Get
         */
                
        private PessoaDbContext _pessoaDbContext;
        private IMapper _mapper;

        public PessoaController(PessoaDbContext context, IMapper mapper)
        {
            _pessoaDbContext = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma pessoa ao banco de dados
        /// </summary>
        /// <param name="pessoaDto">Objeto com os campos necessários para criação do registro de uma pessoa</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        /// <response code="400">Caso existam dados inválidos</response>
        /// <response code="500">Caso ocorra erro interno do servidor</response>
        /// [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("cadastrapessoa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CadastrarPessoa([FromBody] CreatePessoaDto pessoaDto)
        {
            Pessoa pessoa = _mapper.Map<Pessoa>(pessoaDto);
            _pessoaDbContext.Pessoas.Add(pessoa);
            _pessoaDbContext.SaveChanges();
            return CreatedAtAction(nameof(RecuperaPessoaPorId),
                new { id = pessoa.Id },
                pessoa);
        }

        /// <summary>
        /// Busca uma pessoa no banco de dados
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será buscado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>
        /// <response code="404">Caso existam dados inválidos</response>
        /// [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("buscapessoaporid/{id}")]
        public IActionResult RecuperaPessoaPorId(int id)
        {
            var pessoa = _pessoaDbContext.Pessoas
                .FirstOrDefault(pessoa => pessoa.Id == id);
            if (pessoa == null) return NotFound();
            var pessoaDto = _mapper.Map<ReadPessoaDto>(pessoa);
            return Ok(pessoaDto);
        }

        /// <summary>
        /// Atualiza um registro no banco de dados
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será atualizado</param>
        /// <param name="pessoaDto">Objeto com os campos necessários para atualização do registro</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso inserção seja feita com sucesso (sem conteúdo)</response>
        /// <response code="400">Caso existam dados inválidos</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("atualizapessoa/{id}")]
        public IActionResult AtualizaPessoa(int id, [FromBody] UpdatePessoaDto pessoaDto)
        {
            var pessoa = _pessoaDbContext.Pessoas.FirstOrDefault(
                pessoa => pessoa.Id == id);
            if (pessoa == null) return NotFound();
            _mapper.Map(pessoaDto, pessoa);
            _pessoaDbContext.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza um registro parcialmente no banco de dados
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será atualizado</param>
        /// <param name="patch">Objeto com os campos necessários para atualização do registro</param>
        /// <returns>IActionResult</returns>
        ///// <response code="200">Caso inserção seja feita com sucesso</response>
        /// <response code="204">Caso inserção seja feita com sucesso (sem conteúdo)</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("atualizapessoaparcial/{id}")]
        public IActionResult AtualizaPessoaParcial(int id, JsonPatchDocument<UpdatePessoaDto> patch)
        {
            var pessoa = _pessoaDbContext.Pessoas.FirstOrDefault(
                pessoa => pessoa.Id == id);
            if (pessoa == null) return NotFound();

            var pessoaParaAtualizar = _mapper.Map<UpdatePessoaDto>(pessoa);

            patch.ApplyTo(pessoaParaAtualizar, ModelState);

            if (!TryValidateModel(pessoaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(pessoaParaAtualizar, pessoa);
            _pessoaDbContext.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// Remove um registro do banco de dados
        /// </summary>
        /// <param name="id">Parâmetro que indica qual registro será removido</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso remoção seja feita com sucesso (sem conteúdo)</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{id}")]
        public IActionResult DeletaPessoa(int id)
        {
            var pessoa = _pessoaDbContext.Pessoas.FirstOrDefault(
                pessoa => pessoa.Id == id);
            if (pessoa == null) return NotFound();
            _pessoaDbContext.Remove(pessoa);
            _pessoaDbContext.SaveChanges();
            return NoContent();
        }

    }
}
