using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        //Método para mostrar todas as pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDTO>>> Get()
        {
            try
            {
                var pessoas = await _pessoaService.GetPessoas();
                return Ok(pessoas);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Método para motrar pessoa com base em seu id
        [HttpGet("{id}", Name = "GetPessoa")]
        public async Task<ActionResult<Pessoa>> GetById(int id)
        {
            var pessoa = await _pessoaService.GetById(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        //Método para montar um relatório de despesas das pessoas
        [HttpGet("RelatorioDespesasPessoas")]
        public async Task<ActionResult<IEnumerable<RelatorioPessoasDTO>>> GetDespesasRelatorio()
        {
            try
            {
                var relatorio = await _pessoaService.GetRelatorioPessoas();
                return Ok(relatorio);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        //Método de cadastro de pessoa
        [HttpPost]
        public async Task<ActionResult> Post(PessoaDTO pessoaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _pessoaService.Add(pessoaDto);
                return new CreatedAtRouteResult("GetPessoa", new { id = pessoaDto.PessoaId }, pessoaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para alteração de uma pessoa
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PessoaDTO pessoaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != pessoaDto.PessoaId)
                {
                    return BadRequest();
                }
                await _pessoaService.Update(pessoaDto);
                return Ok(pessoaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Método para excluir uma pessoa
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pessoaDto = await _pessoaService.GetById(id);
            if (pessoaDto == null)
            {
                return NotFound();
            }

            await _pessoaService.Delete(id);
            return NoContent();
        }
    }
}
